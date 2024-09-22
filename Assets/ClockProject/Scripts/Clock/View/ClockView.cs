using System;
using Clock.Controller;
using TMPro;
using UnityEngine;

namespace Clock.View
{
    public class ClockView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _timeText;
        private ClockController _clockController;
        private bool _isConstructed;

        public void Construct(ClockController clockController)
        {
            _clockController = clockController;
            _isConstructed = true;
            
            _clockController.TimeUpdated += UpdateClockText;
        }
        
        private void OnEnable()
        {
            if (_isConstructed)
                _clockController.TimeUpdated += UpdateClockText;
        }

        private void OnDisable()
        {
            _clockController.TimeUpdated -= UpdateClockText;
        }

        private void UpdateClockText(DateTime newTime)
        {
            _timeText.text = newTime.ToString("HH:mm:ss");
        }
    }
}