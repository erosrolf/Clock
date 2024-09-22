using System;

namespace Clock.Model
{
    public interface ITimeData
    {
        public DateTime CurrentTime { get; set; }
    }
}