using DG.Tweening;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class UpperLogo : UIAnimation
{
    [SerializeField] private CanvasGroup _back;
    [SerializeField] private GameObject _logo;
    
    [SerializeField] private Transform _startBack;
    [SerializeField] private Transform _doBack;

    private void Awake()
    {
        _back.transform.position = _startBack.position;
        _back.alpha = 0;
        _logo.transform.localScale = Vector3.zero;
    }

    public override void Animate()
    {
        _logo.transform.DOScale(Vector3.one, 0.3f);
        _logo.transform.DOPunchRotation(Vector3.one * 3, 0.3f).SetDelay(0.3f);

        _back.DOFade(1f, 0.2f).SetDelay(0.3f);
        _back.transform.DOMove(_doBack.position, 0.3f).SetDelay(0.3f);
    }
}
