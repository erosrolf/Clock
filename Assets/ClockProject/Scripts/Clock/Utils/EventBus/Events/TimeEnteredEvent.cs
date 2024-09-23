namespace Clock.Utils.EventBus
{
    public class TimeEnteredEvent
    {
        public string TimeString { get; }

        public TimeEnteredEvent(string timeString)
        {
            TimeString = timeString;
        }
    }
}