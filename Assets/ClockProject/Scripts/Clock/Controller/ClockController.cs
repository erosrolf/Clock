using System;
using Clock.Model;
using Clock.Services.TimeProvider;
using Clock.Services.TimerService;
using Clock.Utils.EventBus;
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

        private IDisposable _subscription;
        
        public ClockController(ClockModel clockModel)
        {
            _clockModel = clockModel;
            _timeProviderService = new TimeProviderService(new YandexTimeProvider());
            _timerService = new TimerService();
            
            EventBus.Subscribe<TimeEnteredEvent>(TimeCorrection);
        }
        
        public async UniTask SynchronizeTime()
        {
            _clockModel.ActualTime = await _timeProviderService.GetCurrentTimeAsync();
            TimeUpdated?.Invoke(_clockModel.CurrentTime);
        }
        
        public void UpdateOffset(TimeSpan offset)
        {
            _clockModel.TimeOffset = offset;
            TimeUpdated?.Invoke(_clockModel.CurrentTime);
        }
        
        public void StartTimer()
        {
            _timerService.StartTimer(_clockModel.ActualTime);
        }

        public void StopTimer()
        {
            _timerService.StopTimer();
        }

        public void StartUpdateView()
        {
            _subscription?.Dispose();
            _subscription = _timerService.DateTimeObservable
                .Subscribe(actualTime =>
                {
                    _clockModel.ActualTime = actualTime;
                    TimeUpdated?.Invoke(_clockModel.CurrentTime);
                });
        }

        public void StopUpdateView()
        {
            _subscription?.Dispose();
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

        public void Dispose()
        {
            EventBus.Unsubscribe<TimeEnteredEvent>(TimeCorrection);
        }
    }
}