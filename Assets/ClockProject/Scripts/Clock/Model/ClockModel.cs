using System;
using UnityEngine;

namespace Clock.Model
{
    [Serializable]
    public class ClockModel : ITimeData
    {
        /// <summary>
        /// ActualTime from server.
        /// </summary>
        public DateTime ActualTime;
        /// <summary>
        /// User settings for time offset.
        /// </summary>
        public TimeSpan TimeOffset;
        /// <summary>
        /// Local utc offset.
        /// </summary>
        public TimeSpan UtcOffset;
        
        /// <summary>
        /// Current user setting time adjusted to utc.
        /// </summary>
        public DateTime CurrentTime => ActualTime + TimeOffset + UtcOffset;

        public override string ToString()
        {
            return CurrentTime.ToString("HH:mm:ss");
        }
    }
}