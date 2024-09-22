using Clock.Model;

namespace Clock.Services.TimerService
{
    public interface ITimerService
    {
        ITimeData CurrentTimeData { get; }
        void StartTimer(ITimeData initialTimeData);
        void StopTimer();
    }
}