using System;
using Clock.Utils;
using Clock.Utils.EventBus;
using UnityEngine;
using UnityEngine.UI;

namespace Clock.Services.TimeCorrection
{
    public class ManualTimeAdjustmentManager : MonoBehaviour
    {
        [SerializeField] private TMPInputValidator _tmpInputValidator;
        [SerializeField] private ToggleButton _activateButton;

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