using UnityEngine;
using UnityEngine.UI;

public class Message : MonoBehaviour
{
    [SerializeField] private Text _text;

    public void SetText(string textValue)
    {
        _text.text = textValue;
    }
}
