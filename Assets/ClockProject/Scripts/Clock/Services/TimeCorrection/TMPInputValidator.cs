using System;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;
using System.Collections;
using Utils.EventBus;
using UnityEngine.UI;

namespace Clock.Services.TimeCorrection
{
    /// <summary>
    /// Validates time input from a TMP_InputField and notifies when a valid time is entered.
    /// </summary>
    public class TMPInputValidator : MonoBehaviour
    {
        private TMP_InputField _inputField;
        private Image _image;
        private Color _originalColor;

        #region MONO
        private void Awake()
        {
            _inputField = GetComponent<TMP_InputField>();
            _image = GetComponent<Image>();

            _originalColor = _image.color;
        }

        private void OnEnable()
        {
            _inputField.onEndEdit.AddListener(ValidateTimeInput);
        }

        private void OnDisable()
        {
            _inputField.onEndEdit.RemoveListener(ValidateTimeInput);
        }
        #endregion

        /// <summary>
        /// Validates the input text against a time format regex pattern.
        /// If valid, publishes a TimeEnteredEvent; if invalid, triggers visual feedback.
        /// </summary>
        /// <param name="inputText">The input text to validate.</param>
        private void ValidateTimeInput(string inputText)
        {
            string pattern = @"^(?:(?:([01]?\d|2[0-3]):)?([0-5]?\d):)?([0-5]?\d)$";

            if (Regex.IsMatch(inputText, pattern))
            {
                EventBus.Publish(new TimeEnteredEvent(inputText));
            }
            else
            {
                Debug.Log("Input text is invalid");
                StartCoroutine(FlashInputField());
                _inputField.text = String.Empty;
            }
        }

        private IEnumerator FlashInputField()
        {
            _image.color = Color.red;
            yield return new WaitForSeconds(0.3f);
            _image.color = _originalColor;
        }
    }
}
