using System;
using Clock.Model;
using Clock.Services.TimeProvider;
using Cysharp.Threading.Tasks;

namespace Clock.Controller
{
    public class ClockController
    {
        public event Action<DateTime> TimeUpdated;
        
        private TimeProviderService _timeProviderService;
        private ClockModel _clockModel;

        public ClockController(ClockModel clockModel)
        {
            _clockModel = clockModel;
            _timeProviderService = new TimeProviderService(new YandexTimeProvider());
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