using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using R3;

namespace Clock.Services.TimerService
{
    public class TimerService : ITimerService
    {
        public Observable<DateTime> DateTimeObservable => _currentTimeProperty;
        public event Action OnHourHasPassed;
        
        private ReactiveProperty<DateTime> _currentTimeProperty = new();
        private CancellationTokenSource _cts;
        private DateTime _lastHourCheck;

        public void StartTimer(DateTime initialTime)
        {
            _currentTimeProperty = new ReactiveProperty<DateTime>(initialTime);
            _lastHourCheck = initialTime;

            _cts = new CancellationTokenSource();
            UpdateTimeAsync(_cts.Token);
        }

        public void StopTimer()
        {
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;
        }

        private async void UpdateTimeAsync(CancellationToken ct)
        {
            while (!ct.IsCancellationRequested)
            {
                _currentTimeProperty.OnNext(_currentTimeProperty.Value.AddSeconds(1));

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