using System;
using Clock.Model;
using Clock.Services.TimeProvider;
using Clock.Services.TimerService;
using Cysharp.Threading.Tasks;
using R3;

namespace Clock.Controller
{
    public class ClockController
    {
        public event Action<DateTime> TimeUpdated;
        
        private ClockModel _clockModel;
        private TimeProviderService _timeProviderService;
        private TimerService _timerService;

        private CompositeDisposable _subscriptions;
        
        public ClockController(ClockModel clockModel)
        {
            _clockModel = clockModel;
            _timeProviderService = new TimeProviderService(new YandexTimeProvider());
            _timerService = new TimerService();
        }
        
        public void StartTimer()
        {
            _subscriptions?.Dispose();
            _subscriptions = new CompositeDisposable();
            _timerService.StartTimer(_clockModel.CurrentTime);
            _timerService.DateTimeObservable
                .Subscribe(newValue => UpdateTime(newValue))
                .AddTo(_subscriptions);
        }

        public async UniTask SynchronizeTime()
        {
            UpdateTime(await _timeProviderService.GetCurrentTimeAsync());
        }
        
        public void UpdateTime(DateTime newTime)
        {
            _clockModel.CurrentTime = newTime;
            TimeUpdated?.Invoke(newTime);
        }
    }
}