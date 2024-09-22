using System;
using Clock.Model;

namespace Clock.Services.TimerService
{
    public interface ITimerService
    {
        void StartTimer(DateTime initialTime);
        void StopTimer();
    }
}