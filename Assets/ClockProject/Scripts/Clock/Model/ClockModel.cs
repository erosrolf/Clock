using System;
using UnityEngine;

namespace Clock.Model
{
    [Serializable]
    public class ClockModel : ITimeData
    {
        [SerializeField] private DateTime _currentTime;
        public DateTime CurrentTime { get => _currentTime; set => _currentTime = value; }

        public override string ToString()
        {
            return _currentTime.ToString("HH:mm:ss");
        }
    }
}