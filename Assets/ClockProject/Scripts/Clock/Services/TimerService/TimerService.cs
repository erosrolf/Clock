using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using R3;

namespace Clock.Services.TimerService
{
    /// <summary>
    /// Implementation of the ITimerService interface that manages a timer and updates time.
    /// </summary>
    public class TimerService : ITimerService
    {
        
        // Observable to expose the current time.
        public Observable<DateTime> DateTimeObservable => _currentTimeProperty;
        
        // Event triggered when an hour has passed.
        public event Action OnHourHasPassed;
        
        private ReactiveProperty<DateTime> _currentTimeProperty = new();
        private CancellationTokenSource _cts;
        private DateTime _lastHourCheck;
        
        /// <summary>
        /// Starts the timer with the specified initial time.
        /// </summary>
        /// <param name="initialTime">The starting DateTime for the timer.</param>
        public void StartTimer(DateTime initialTime)
        {
            _currentTimeProperty = new ReactiveProperty<DateTime>(initialTime);
            _lastHourCheck = initialTime;

            _cts = new CancellationTokenSource();
            UpdateTimeAsync(_cts.Token);
        }

        /// <summary>
        /// Stops the currently running timer.
        /// </summary>
        public void StopTimer()
        {
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;
        }
        
        /// <summary>
        /// Updates the time every second and checks if an hour has passed.
        /// </summary>
        /// <param name="ct">Cancellation token to control the update loop.</param>
        private async void UpdateTimeAsync(CancellationToken ct)
        {
            while (!ct.IsCancellationRequested)
            {
                _currentTimeProperty.OnNext(_currentTimeProperty.Value.AddSeconds(1));

                // Check if an hour has passed since last check.
                if (_lastHourCheck.Hour != _currentTimeProperty.Value.Hour)
                {
                    OnHourHasPassed?.Invoke();
                    _lastHourCheck = _currentTimeProperty.Value;
                }
                
                await UniTask.Delay(TimeSpan.FromSeconds(1), cancellationToken: ct);
            }
        }
    }
}