using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class StartScreen : MonoBehaviour
{
    [SerializeField] private Image _pickGroup;
    [SerializeField] private Image _logo;
    [SerializeField] private Text _text;

    [SerializeField] private CanvasGroup _panel;
    
    [SerializeField] private Transform _startChanger;
    [SerializeField] private Transform _doChanger;
        
    [SerializeField] private Transform _startLogo;
    [SerializeField] private Transform _doLogo;

    [SerializeField] private Dropdown _dropdown;
    
    private void Awake()
    {
        PlayAnimation();
        _dropdown.value = 77;
    }

    private void OnEnable()
    {
        _dropdown.onValueChanged.AddListener(OnGroupSelected);
    }

    private void PlayAnimation()
    {
        _logo.transform.position = _startLogo.position;
        _pickGroup.transform.position = _startChanger.position;
        _logo.DOFade(1f, 0.5f);
        _pickGroup.DOFade(1f, 0).SetDelay(0.5f);

        _logo.transform.DOMove(_doLogo.position, 0.5f).SetDelay(0.5f);
        _pickGroup.transform.DOMove(_doChanger.position, 0.5f).SetDelay(0.5f);

        _text.DOFade(1f, 0.2f).SetDelay(1f);
    }

    private void OnGroupSelected(int index)
    {
        
        
        Debug.Log($"Selected group index: {index}");
        _panel.DOFade(0, 0.5f).SetDelay(0.3f);
    }
}
