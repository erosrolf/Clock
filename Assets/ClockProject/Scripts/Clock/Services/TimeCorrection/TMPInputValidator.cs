using System;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;
using System.Collections;
using UnityEngine.UI;

namespace Clock.Services.TimeCorrection
{
    public class TMPInputValidator : MonoBehaviour
    {
        private TMP_InputField _inputField;
        private Image _image;
        private Color _originalColor;

        private void Start()
        {
            _inputField = GetComponent<TMP_InputField>();
            _image = GetComponent<Image>();

            _originalColor = _image.color;
            _inputField.onEndEdit.AddListener(ValidateTimeInput);
        }

        private void ValidateTimeInput(string inputText)
        {
            string pattern = @"^(?:(?:([01]?\d|2[0-3]):)?([0-5]?\d):)?([0-5]?\d)$";

            if (!Regex.IsMatch(inputText, pattern))
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
