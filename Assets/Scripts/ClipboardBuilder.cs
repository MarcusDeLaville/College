using System;
using System.Collections.Generic;
using DG.Tweening;
using MagneticScrollView;
using UnityEngine;
using UnityEngine.UI;

public class ClipboardBuilder : MonoBehaviour
{
    [SerializeField] private Clipboard _clipboard;
    [SerializeField] private Dropdown _groupChanger;
    [SerializeField] private GroupsPicker _groupsPicker;
    
    [SerializeField] private Transform _daysCorner;

    [SerializeField] private int _currentGroup;
    [SerializeField] private int _currentDay;
    
    [SerializeField] private List<DayUI> _dayUis;

    [SerializeField] private DayUI _dayPrefab;
    [SerializeField] private LessonUI _lessonPrefab;

    [SerializeField] private Button _back;
    [SerializeField] private CanvasGroup _startPanel;

    [SerializeField] private SwipeDetection _swipeDetection;
    
    private void Awake()
    {
        _groupsPicker.SetGroups(_clipboard.Groups);
    }

    private void OnEnable()
    {
        _groupsPicker.GroupPick += OnGroupPicked;
        _back.onClick.AddListener(() => _startPanel.DOFade(1f, 0.5f));
        // попробовать принимать один колбек свайпа и перенести в единый метод реагирования
        _swipeDetection.swipeEvents[0].callback.AddListener(OnSwipeLeft);
        _swipeDetection.swipeEvents[1].callback.AddListener(OnSwipeRight);
    }

    private void OnDisable()
    {
        _groupsPicker.GroupPick -= OnGroupPicked;
    }

    private void OnGroupPicked(int index)
    {
        _currentGroup = index;
        _currentDay = 0;
        
        ClearAll();
        SpawnDays();

        DelayedCallUtil.DelayedCall(0.7f, () => ShowLessons());
    }

    //todo: работает но это ебаные спагетти
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

    private void ShowLessons()
    {
        float delay = 0.3f;
        float step = 0.3f;
        
        for (int i = 0; i < _dayUis[_currentDay].Lessons.Count; i++)
        {
            _dayUis[_currentDay].Lessons[i].CanvasGroup.DOFade(1f, delay + (step * i));
        }
    }

    //ынести свайпы в другой класс
    private void OnSwipeLeft()
    {
        if(_currentDay == _dayUis.Count-1) return;
        _currentDay++;
        ShowLessons();
    }
    
    private void OnSwipeRight()
    {
        if(_currentDay == 0) return;
        
        _currentDay--;
        ShowLessons();
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
