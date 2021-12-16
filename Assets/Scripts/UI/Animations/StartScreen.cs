using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class StartScreen : MonoBehaviour
{
    [SerializeField] private Image _background;
    [SerializeField] private Image _logo;

    private void Awake()
    {
        PlayAnimation();
    }

    private void PlayAnimation()
    {
        _logo.transform.DOPunchScale(Vector3.one / 3, 0.6f, 1, 1f).SetDelay(0.1f);
        _logo.DOFade(0.8f, 0.4f).SetLoops(4, LoopType.Yoyo).onComplete += () =>
        {
            _logo.DOFade(0f, 0.2f);
            _background.DOFade(0f, 0.2f);
        };
    }
}
