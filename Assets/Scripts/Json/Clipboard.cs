using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Clipboard : MonoBehaviour
{
    [SerializeField] private string _savePath;
    [SerializeField] private string _saveFileName = "data.json";

    [SerializeField] private Clipbooard _currentClipdoard;
    
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
        Clipbooard clipbooard = new Clipbooard
        {
            GroupsName = this._currentClipdoard.GroupsName,     
            Lessons = this._currentClipdoard.Lessons
        };
    
        string json = JsonUtility.ToJson(clipbooard, true);
        File.WriteAllText(_savePath, json);
    }
    
    [ContextMenu("Load")]
    public void LoadConfig()
    {
        if (!File.Exists(_savePath))
        {
        }
    
        string json = File.ReadAllText(_savePath);
        Clipbooard _coinsFromJson = JsonUtility.FromJson<Clipbooard>(json);

        _currentClipdoard.GroupsName = _coinsFromJson.GroupsName;
        _currentClipdoard.Lessons = _coinsFromJson.Lessons;
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
    public struct Clipbooard
    {
        public List<string> GroupsName;
        public List<Lesson> Lessons;
    }

    [Serializable]
    public struct Lesson
    {
        public string LessonName;
        public string Professor;
    }
