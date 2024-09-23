using System;
using Clock.Controller;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Clock.View
{
    public class ClockView : MonoBehaviour
    {
        [SerializeField] private Transform _hourHand;
        [SerializeField] private Transform _minuteHand;
        
        [SerializeField] private TextMeshProUGUI _timeText;
        private ClockController _clockController;
        private bool _isConstructed;

        public void Construct(ClockController clockController)
        {
            _clockController = clockController;
            _isConstructed = true;
            
            _clockController.TimeUpdated += UpdateViews;
            
            _hourHand.localRotation = Quaternion.Euler(0, 0, 90);
        }
        
        private void OnEnable()
        {
            if (_isConstructed)
                _clockController.TimeUpdated += UpdateViews;
        }

        private void OnDisable()
        {
            _clockController.TimeUpdated -= UpdateViews;
        }

        public void UpdateViews(DateTime newTime)
        {
            UpdateClockText(newTime);
            SetHourHandRotation(newTime);
            SetMinuteHandRotation(newTime);
        }

        private void UpdateClockText(DateTime newTime)
        {
            _timeText.text = newTime.ToString("HH:mm:ss");
        }

        private void SetHourHandRotation(DateTime newTime)
        {
            float hourAngle = (newTime.Hour % 12) * 30 + newTime.Minute * 0.5f; // Правильный расчет угла
            _hourHand.DORotate(new Vector3(0, 180, hourAngle), 0.5f);
        }

        private void SetMinuteHandRotation(DateTime NewTime)
        {
            float minuteAngle = NewTime.Minute * 6 + NewTime.Second * 0.1f;
            _minuteHand.DORotate(new Vector3(0, 180, minuteAngle), 0.5f);
        }
    }
}