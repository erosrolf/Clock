using System;

namespace Clock.Services.TimerService
{
    /// <summary>
    /// Defines the contract for a timer service that controls timer operations.
    /// </summary>
    public interface ITimerService
    {
        /// <summary>
        /// Starts the timer with the specified initial time.
        /// </summary>
        /// <param name="initialTime">The DateTime to set as the initial time for the timer.</param>
        void StartTimer(DateTime initialTime);

        /// <summary>
        /// Stops the currently running timer.
        /// </summary>
        void StopTimer();
    }
}
