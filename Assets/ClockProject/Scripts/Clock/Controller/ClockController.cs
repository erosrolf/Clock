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
        
        private readonly ClockModel _clockModel;
        private readonly TimeProviderService _timeProviderService;
        private readonly TimerService _timerService;
        private readonly CompositeDisposable _compositeDisposable;

        public ClockController(ClockModel clockModel)
        {
            _clockModel = clockModel;
            _timeProviderService = new TimeProviderService(new YandexTimeProvider());
            _timerService = new TimerService();
            _compositeDisposable = new CompositeDisposable();

            SubscribeToEvents();
        }

        /// <summary>
        /// Synchronizes time from the time provider service.
        /// </summary>
        public async UniTask SynchronizeTime()
        {
            try
            {
                _clockModel.ActualTime = await _timeProviderService.GetCurrentTimeAsync();
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                _clockModel.ActualTime = DateTime.Now; // Сохраняем локальное время в случае ошибки
                Debug.Log("Time was set from local data");
            }
            TimeUpdated?.Invoke(_clockModel.CurrentTime);
        }

        /// <summary>
        /// Updates the clock offset.
        /// </summary>
        public void UpdateOffset(TimeSpan offset)
        {
            _clockModel.TimeOffset = offset;
            TimeUpdated?.Invoke(_clockModel.CurrentTime);
        }

        /// <summary>
        /// Starts the clock timer and subscribes to time updates.
        /// </summary>
        public void StartClock()
        {
            _timerService.StartTimer(_clockModel.ActualTime);

            SubscribeToTimeUpdates(); // Подписываемся на обновление времени
            _timerService.OnHourHasPassed += OnHourHasPassedHandler; // Подписываемся на событие
        }

        /// <summary>
        /// Corrections the time based on the entered event.
        /// </summary>
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

        /// <summary>
        /// Subscribes to necessary events.
        /// </summary>
        private void SubscribeToEvents()
        {
            EventBus.Subscribe<TimeEnteredEvent>(TimeCorrection);
        }

        /// <summary>
        /// Subscribes to time updates from the timer service.
        /// </summary>
        private void SubscribeToTimeUpdates()
        {
            _timerService.DateTimeObservable
                .Subscribe(UpdateTime)
                .AddTo(_compositeDisposable);
        }

        /// <summary>
        /// Updates the time and invokes the TimeUpdated event.
        /// </summary>
        private void UpdateTime(DateTime actualTime)
        {
            _clockModel.ActualTime = actualTime;
            TimeUpdated?.Invoke(_clockModel.CurrentTime);
        }

        public void Dispose()
        {
            _compositeDisposable?.Dispose();
            _timerService.OnHourHasPassed -= OnHourHasPassedHandler;
            EventBus.Unsubscribe<TimeEnteredEvent>(TimeCorrection);
        }
    }
}
