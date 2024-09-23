using System;
using UnityEngine;

namespace Clock.Model
{
    [Serializable]
    public class ClockModel : ITimeData
    {
        public DateTime ActualTime;
        public TimeSpan TimeOffset;
        public TimeSpan UtcOffset;
        
        public DateTime CurrentTime => ActualTime + TimeOffset;

        public override string ToString()
        {
            return CurrentTime.ToString("HH:mm:ss");
        }
    }
}