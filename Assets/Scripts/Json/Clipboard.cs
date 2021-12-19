using System;
using System.Collections.Generic;
using System.IO;
using Sirenix.OdinInspector;
using UnityEngine;

public class Clipboard : MonoBehaviour
{
    [SerializeField] private string _savePath;
    [SerializeField] private string _saveFileName = "data.json";
    
    [SerializeField] private List<Group>  _groups;

    public List<Group> Groups => _groups;
    
    private void Awake()
    {
    #if UNITY_ANDROID && !UNITY_EDITOR
        _savePath = Path.Combine(Application.persistentDataPath, _saveFileName);
    #else
        _savePath = Path.Combine(Application.dataPath, _saveFileName);
    #endif
        LoadConfig();
    }
    
    [ContextMenu("Save")]
    public void SaveConfig()
    {
        Group @group = new Group
        {
            // GroupsName = this._currentClipdoard.GroupsName,     
            // Lessons = this._currentClipdoard.Lessons
        };
    
        string json = JsonUtility.ToJson(@group, true);
        File.WriteAllText(_savePath, json);
    }
    
    [ContextMenu("Load")]
    public void LoadConfig()
    {
        if (!File.Exists(_savePath))
        {
        }
    
        string json = File.ReadAllText(_savePath);
        Group _coinsFromJson = JsonUtility.FromJson<Group>(json);

        // _currentClipdoard.GroupsName = _coinsFromJson.GroupsName;
        // _currentClipdoard.Lessons = _coinsFromJson.Lessons;
    }
    
    private void OnApplicationQuit()
    {
        SaveConfig();
    }
    
    private void OnApplicationPause(bool pause)
    {
        SaveConfig();
    }
    
    }
    
    [Serializable]
    public struct Group
    {
        public string Name;
        public List<Week> Weeks;
    }

    [Serializable]
    public struct Week
    {
        public List<Day> Days;
    }

    [Serializable]
    public struct Day
    {
        public string Name;
        public List<Lesson> Lessons;
    }

    [Serializable]
    public struct Lesson
    {
        public string LessonName;
        public string Professor;
        public string Auditory;
    }
    
    
