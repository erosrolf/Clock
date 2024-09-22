using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using R3;

namespace Clock.Services.TimerService
{
    public class TimerService : ITimerService
    {
        public Observable<DateTime> DateTimeObservable => _currentTimeProperty;
        
        private ReactiveProperty<DateTime> _currentTimeProperty;
        private CancellationTokenSource _cts;
        
        public void StartTimer(DateTime initialTime)
        {
            _currentTimeProperty = new ReactiveProperty<DateTime>(initialTime);

            if (_cts != null)
            {
                StopTimer();
            }
            
            _cts = new CancellationTokenSource();
            UpdateTimeAsync(_cts.Token);
        }

        public void StopTimer()
        {
            _cts?.Cancel();
            _cts = null;
        }

        private async void UpdateTimeAsync(CancellationToken ct)
        {
            while (!ct.IsCancellationRequested)
            {
                _currentTimeProperty.OnNext(_currentTimeProperty.Value.AddSeconds(1));
                await UniTask.Delay(TimeSpan.FromSeconds(1), cancellationToken: ct);
            }
        }
    }
}