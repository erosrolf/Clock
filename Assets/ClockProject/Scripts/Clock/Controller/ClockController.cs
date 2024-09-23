using System;
using Clock.Model;
using Clock.Services.TimeProvider;
using Clock.Services.TimerService;
using Utils.EventBus;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;

namespace Clock.Controller
{
    public class ClockController : IDisposable
    {
        public event Action<DateTime> TimeUpdated;
        
        private ClockModel _clockModel;
        private TimeProviderService _timeProviderService;
        private TimerService _timerService;

        private TimeSpan _utcOffset;
        private CompositeDisposable _compositeDisposable;
        
        public ClockController(ClockModel clockModel)
        {
            _clockModel = clockModel;
            _timeProviderService = new TimeProviderService(new YandexTimeProvider());
            _timerService = new TimerService();
            _compositeDisposable = new CompositeDisposable();
            
            EventBus.Subscribe<TimeEnteredEvent>(TimeCorrection);
        }
        
        public async UniTask SynchronizeTime()
        {
            try
            {
                _clockModel.ActualTime = await _timeProviderService.GetCurrentTimeAsync(_clockModel.UtcOffset);
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                _clockModel.ActualTime = DateTime.Now;
                Debug.Log("Time was set from local data");
            }
            TimeUpdated?.Invoke(_clockModel.CurrentTime);
        }
        
        public void UpdateOffset(TimeSpan offset)
        {
            _clockModel.TimeOffset = offset;
            TimeUpdated?.Invoke(_clockModel.CurrentTime);
        }
        
        public void StartClock()
        {
            _timerService.StartTimer(_clockModel.ActualTime);
            _timerService.DateTimeObservable
                .Subscribe(actualTime =>
                {
                    _clockModel.ActualTime = actualTime;
                    TimeUpdated?.Invoke(_clockModel.CurrentTime);
                }).AddTo(_compositeDisposable);
            _timerService.OnHourHasPassed += OnHourHasPassedHandler;
        }

        private void TimeCorrection(TimeEnteredEvent timeEnteredEvent)
        {
            if (DateTime.TryParse(timeEnteredEvent.TimeString, out DateTime newTime))
            {
                UpdateOffset(newTime - _clockModel.ActualTime);
            }
            else
            {
                Debug.Log("Incorrect DateTime: " + timeEnteredEvent.TimeString);
            }
        }
        private void OnHourHasPassedHandler()
            => _ = SynchronizeTime();

        public void Dispose()
        {
            _compositeDisposable?.Dispose();
            _timerService.OnHourHasPassed -= OnHourHasPassedHandler;
            EventBus.Unsubscribe<TimeEnteredEvent>(TimeCorrection);
        }
    }
}