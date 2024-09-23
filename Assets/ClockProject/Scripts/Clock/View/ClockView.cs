using System;
using Clock.Controller;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Clock.View
{
    /// <summary>
    /// The <c>ClockView</c> class is responsible for rendering the clock's visual representation.
    /// This includes the hour, minute, and second hands as well as displaying the current time.
    /// </summary>
    public class ClockView : MonoBehaviour
    {
        [SerializeField] private Transform _hourHand;
        [SerializeField] private Transform _minuteHand;
        [SerializeField] private Transform _secondHand;
        [SerializeField] private TextMeshProUGUI _timeText;
        
        private ClockController _clockController;
        private bool _isConstructed;

        /// <summary>
        /// Constructs the ClockView with the specified ClockController.
        /// </summary>
        /// <param name="clockController">The ClockController that this view will use for time updates.</param>
        public void Construct(ClockController clockController)
        {
            _clockController = clockController;
            _isConstructed = true;
            
            _clockController.TimeUpdated += UpdateViews;
        }

        #region MONO
        private void OnEnable()
        {
            if (_isConstructed)
                _clockController.TimeUpdated += UpdateViews;
        }

        private void OnDisable()
        {
            _clockController.TimeUpdated -= UpdateViews;
        }
        #endregion

        /// <summary>
        /// Updates the clock visuals and text when the time is updated.
        /// </summary>
        /// <param name="newTime">The new DateTime value representing the current time.</param>
        public void UpdateViews(DateTime newTime)
        {
            UpdateClockText(newTime);
            SetHourHandRotation(newTime);
            SetMinuteHandRotation(newTime);
            SetSecondHandRotation(newTime);
        }

        /// <summary>
        /// Updates the displayed clock text with the current time.
        /// </summary>
        /// <param name="newTime">The new DateTime value for updating the time text.</param>
        private void UpdateClockText(DateTime newTime)
        {
            _timeText.text = newTime.ToString("HH:mm:ss");
        }

        /// <summary>
        /// Sets the rotation of the hour hand based on the current time.
        /// </summary>
        /// <param name="newTime">The current DateTime value used for calculating the rotation.</param>
        private void SetHourHandRotation(DateTime newTime)
        {
            float hourAngle = (newTime.Hour % 12) * 30 + newTime.Minute * 0.5f; // Правильный расчет угла
            _hourHand.DORotate(new Vector3(0, 180, hourAngle), 0.5f);
        }

        /// <summary>
        /// <para>This method follows a similar approach to the <c>SetHourHandRotation</c> method.</para>
        /// </summary>
        private void SetMinuteHandRotation(DateTime NewTime)
        {
            float minuteAngle = NewTime.Minute * 6 + NewTime.Second * 0.1f;
            _minuteHand.DORotate(new Vector3(0, 180, minuteAngle), 0.5f);
        }
        
        /// <summary>
        /// <para>This method follows a similar approach to the <c>SetHourHandRotation</c> method.</para>
        /// </summary>
        private void SetSecondHandRotation(DateTime newTime)
        {
            float secondAngle = newTime.Second * 6;
            _secondHand.DORotate(new Vector3(0, 180, secondAngle), 0.5f);
        }
    }
}