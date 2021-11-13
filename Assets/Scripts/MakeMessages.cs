using System;
using UnityEngine;
using UnityEngine.UI;

public class MakeMessages : MonoBehaviour
{
    [SerializeField] private InputField _inputField;
    [SerializeField] private Button _button;

    [SerializeField] private Transform _parent;
    [SerializeField] private Message _message;

    private void Start()
    {
        _button.onClick.AddListener(MakeMessage);
    }

    private void MakeMessage()
    {
        if (_inputField.text != "")
        {
            Message message = Instantiate(_message, _parent.position, Quaternion.identity, _parent);
            message.SetText(_inputField.text);
            _inputField.text = "";
        }
    }
}
