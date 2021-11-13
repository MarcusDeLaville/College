using System;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.UI;

public class StudentDataDrawer : MonoBehaviour
{
    [SerializeField] private GetStudentData _studentData;
    
    [SerializeField] private Text _groupName;
    [SerializeField] private Text _displayName;
    
    private void OnEnable()
    {
        _studentData.DataRecieved += DrawData;
    }

    private void OnDisable()
    {
        _studentData.DataRecieved -= DrawData;
    }

    private void DrawData(StudentData studentData)
    {
        _groupName.text = studentData.GroupName;
        _displayName.text = studentData.DisplayName;
    }
}
