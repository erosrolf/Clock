using System;
using Clock.Model;
using Clock.Services;
using Cysharp.Threading.Tasks;

namespace Clock.Controller
{
    public class ClockController
    {
        public event Action<DateTime> TimeUpdated;
        
        private TimeService _timeService;
        private ClockModel _clockModel;

        public ClockController(ClockModel clockModel)
        {
            _clockModel = clockModel;
            _timeService = new TimeService(new YandexTimeProvider());
        }

        public async UniTask SynchronizeTime()
        {
            UpdateTime(await _timeService.GetCurrentTimeAsync());
        }
        public void UpdateTime(DateTime newTime)
        {
            _clockModel.CurrentTime = newTime;
            TimeUpdated?.Invoke(newTime);
        }
    }
}