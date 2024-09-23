using System;
using Utils;
using Utils.EventBus;
using UnityEngine;
using UnityEngine.UI;

namespace Clock.Services.TimeCorrection
{
    /// <summary>
    /// Manages custom time settings mode.
    /// </summary>
    public class ManualTimeAdjustmentManager : MonoBehaviour
    {
        [SerializeField] private TMPInputValidator _tmpInputValidator;
        [SerializeField] private ToggleButton _activateButton;

        #region MONO
        private void Start()
        {
            _tmpInputValidator.gameObject.SetActive(false);
        }
        
        private void OnEnable()
        {
            EventBus.Subscribe<TimeEnteredEvent>(_ => _activateButton.SetButtonStatus(false));
            _activateButton.OnButtonToggled += HandleButtonToggled;
        }

        private void OnDisable()
        {
            EventBus.Unsubscribe<TimeEnteredEvent>(_ => _activateButton.SetButtonStatus(false));
            _activateButton.OnButtonToggled -= HandleButtonToggled;
        }
        #endregion

        private void HandleButtonToggled(bool isPressed)
        {
            if (isPressed)
            {
                StartTimeCorrection();
            }
            else
            {
                EndTimeCorrection();
            }
        }

        private void StartTimeCorrection()
        {
            _tmpInputValidator.gameObject.SetActive(true);
        }

        private void EndTimeCorrection()
        {
            _tmpInputValidator.gameObject.SetActive(false);
        }
    }
}