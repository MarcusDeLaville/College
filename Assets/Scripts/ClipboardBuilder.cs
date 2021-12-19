using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ClipboardBuilder : MonoBehaviour
{
    [SerializeField] private Clipboard _clipboard;
    [SerializeField] private Dropdown _groupChanger;

    [SerializeField] private Transform _daysCorner;

    [SerializeField] private int _currentGroup;

    [SerializeField] private List<DayUI> _dayUis;

    [SerializeField] private DayUI _dayPrefab;
    [SerializeField] private LessonUI _lessonPrefab;

    [SerializeField] private Button _back;
    [SerializeField] private CanvasGroup _startPanel;
    
    private void Awake()
    {
        _groupChanger.options.Clear();
        
        foreach (var group in _clipboard.Groups)
        {
            Dropdown.OptionData optionData = new Dropdown.OptionData(group.Name);
            _groupChanger.options.Add(optionData);
        }
    }

    private void OnEnable()
    {
        _groupChanger.onValueChanged.AddListener(OnGroupPicked);
        _back.onClick.AddListener(() => _startPanel.DOFade(1f, 0.5f));

    }

    private void OnDisable()
    {
        _groupChanger.onValueChanged.RemoveListener(OnGroupPicked);
    }

    private void OnGroupPicked(int index)
    {
        _currentGroup = index;
        
        ClearAll();
        SpawnDays();
    }

    private void SpawnDays()
    {
        for (int i = 0; i < _clipboard.Groups[_currentGroup].Weeks[0].Days.Count; i++)
        {
            _dayUis.Add(Instantiate(_dayPrefab, _daysCorner));

            _dayUis[i].DayName.text = _clipboard.Groups[_currentGroup].Weeks[0].Days[i].Name;
            
            for (int j = 0; j < _clipboard.Groups[_currentGroup].Weeks[0].Days[i].Lessons.Count; j++)
            {
                _dayUis[i].Lessons.Add(Instantiate(_lessonPrefab, _dayUis[i].LessonsTransform));

                _dayUis[i].Lessons[_dayUis[i].Lessons.Count -1].AuditoryText.text = _clipboard.Groups[_currentGroup].Weeks[0].Days[i].Lessons[j].Auditory;
                _dayUis[i].Lessons[_dayUis[i].Lessons.Count -1].TeacherNameText.text = _clipboard.Groups[_currentGroup].Weeks[0].Days[i].Lessons[j].Professor;
                _dayUis[i].Lessons[_dayUis[i].Lessons.Count -1].LessonNameText.text = _clipboard.Groups[_currentGroup].Weeks[0].Days[i].Lessons[j].LessonName;

                string time= String.Empty;

                switch (j)
                {
                    case 0:
                        time = "9:00\n - \n10-30";
                        break;
                    case 1:
                        time = "10:45\n - \n12-15";
                        break;
                    case 2:
                        time = "12:45\n - \n14-15";
                        break;
                    case 3:
                        time = "14:30\n - \n16-00";
                        break;
                    case 4:
                        time = "16:15\n - \n17-45";
                        break;
                    case 5:
                        time = "АД";
                        break;
                }

                _dayUis[i].Lessons[_dayUis[i].Lessons.Count - 1].TimeText.text = time;
            }
        }
    }

    private void ClearAll()
    {
        for (int i = 0; i < _dayUis.Count; i++)
        {
            Destroy(_dayUis[i].gameObject);
        }
        
        _dayUis.Clear();
    }
}
