using System;

namespace Clock.Model
{
    /// <summary>
    /// Interface for getting time
    /// </summary>
    public interface ITimeData
    {
        public DateTime CurrentTime { get; }
    }
}