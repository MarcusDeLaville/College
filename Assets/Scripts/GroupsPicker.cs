using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GroupsPicker : MonoBehaviour
{
    public Action<int> GroupPick;

    [SerializeField] private string _defaultGroupOption;
    [SerializeField] private Dropdown _dropdown;

    private int _lastOptionIndex;
    
    private void OnChangeValue(int index)
    {
        if (index != _lastOptionIndex)
        {
            GroupPick?.Invoke(index);
        }
    }

    private void OnEnable()
    {
        _dropdown.onValueChanged.AddListener(OnChangeValue);
    }

    private void OnDisable()
    {
        _dropdown.onValueChanged.RemoveListener(OnChangeValue);
    }

    public void SetGroups(List<Group> groups)
    {
        _dropdown.ClearOptions();
        
        foreach (var group in groups)
        {
            Dropdown.OptionData optionData = new Dropdown.OptionData(group.Name);
            _dropdown.options.Add(optionData);
        }
        
        SetDefaultOption();
    }

    private void SetDefaultOption()
    {
        Dropdown.OptionData optionData = new Dropdown.OptionData(_defaultGroupOption);
        _dropdown.options.Add(optionData);
        
        _lastOptionIndex = _dropdown.options.Count;
         _dropdown.value = _lastOptionIndex;
    }
        
}