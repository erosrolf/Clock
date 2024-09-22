using System;
using Clock.Model;

namespace Clock.Controller
{
    public class TimeController
    {
        public event Action<DateTime> TimeUpdated;
        
        private ClockModel _clockModel;

        public TimeController(ClockModel clockModel)
        {
            _clockModel = clockModel;
        }

        public void UpdateTime(DateTime newTime)
        {
            _clockModel.CurrentTime = newTime;
            TimeUpdated?.Invoke(newTime);
        }
    }
}