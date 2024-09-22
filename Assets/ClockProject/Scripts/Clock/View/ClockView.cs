using System;
using Clock.Controller;
using TMPro;
using UnityEngine;

namespace Clock.View
{
    public class ClockView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _timeText;
        private TimeController _timeController;

        public void Construct(TimeController timeController)
        {
            _timeController = timeController;
        }
        
        private void OnEnable()
        {
            _timeController.TimeUpdated += UpdateTimeText;
        }

        private void OnDisable()
        {
            _timeController.TimeUpdated -= UpdateTimeText;
        }

        private void UpdateTimeText(DateTime newTime)
        {
            _timeText.text = newTime.ToString("HH:mm:ss");
        }
    }
}