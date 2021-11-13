using System;
using UnityEngine;
using UnityEngine.UI;

public class SwitchPanel : MonoBehaviour
{
    [SerializeField] private GameObject[] _panels;

    public void CloseAll()
    {
        foreach (var panel in _panels)
        {
            panel.SetActive(false);
        }
    }
}
