using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Clock.Utils
{
    public class ToggleButton : MonoBehaviour, IPointerDownHandler
    {
        public event Action<bool> OnButtonToggled;

        private bool _isPressed = false;
        private string _originalText;
        private string _toggledText;
        private Button _button;
        private TMP_Text _text;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _text = GetComponentInChildren<TMP_Text>();
            _originalText = _text.text;
            _toggledText = "Pressed";
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _isPressed = !_isPressed;

            OnButtonToggled?.Invoke(_isPressed);
            UpdateView();
        }

        public void SetButtonStatus(bool isPressed)
        {
            _isPressed = false;
            OnButtonToggled?.Invoke(_isPressed);
            UpdateView();
        }

        private void UpdateView()
        {
            _text.text = _isPressed ? _toggledText : _originalText;
            _button.image.color = _isPressed ? Color.gray : Color.white;
        }
    }
}