using System;
using System.Collections;
using UI;
using UnityEngine;

namespace UI
{
    public class UIHandler : MonoBehaviour
    {
        [SerializeField] private UIAnimation firstUiAnimation;

        private void Start()
        {
            firstUiAnimation.Animate();
        }
    }
}