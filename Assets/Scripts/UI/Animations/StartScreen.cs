using DG.Tweening;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class StartScreen : UIAnimation
{
    [SerializeField] private Image _pickGroup;
    [SerializeField] private Image _logo;
    [SerializeField] private Text _hint;

    [SerializeField] private Transform _startChangerPosition;
    [SerializeField] private Transform _doChangerPosition;
        
    [SerializeField] private Transform _startLogoPosition;
    [SerializeField] private Transform _doLogoPosition;
    
    public override void Animate()
    {
        _logo.transform.position = _startLogoPosition.position;
        _pickGroup.transform.position = _startChangerPosition.position;
        _logo.DOFade(1f, 0.5f);
        _pickGroup.DOFade(1f, 0).SetDelay(0.5f);

        _logo.transform.DOMove(_doLogoPosition.position, 0.5f).SetDelay(0.5f);
        _pickGroup.transform.DOMove(_doChangerPosition.position, 0.5f).SetDelay(0.5f);

        _hint.DOFade(1f, 0.2f).SetDelay(1f);
    }
}
