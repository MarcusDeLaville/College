using System;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

[Serializable]
public struct StudentData
{
    public string GroupName;
    public string DisplayName;
}

public class GetStudentData : MonoBehaviour
{
    public Action<StudentData> DataRecieved;
    
    [SerializeField] private PlayFabLogin _fabLogin;

    private LoginResult _loginResult;
    
    private void OnEnable()
    {
        _fabLogin.Logined += GetData;
    }

    private void OnDisable()
    {
        _fabLogin.Logined -= GetData;
    }
    
    private void GetData(LoginResult result)
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnDataRecieved, OnError);
    }

    private void OnDataRecieved(GetUserDataResult result)
    {
        Debug.Log("Data recieved");

        if (result.Data != null)
        {
            DataRecieved?.Invoke(new StudentData {GroupName = result.Data["Group"].Value, DisplayName = result.Data["Name"].Value});
        }
    }
    
    private void OnError(PlayFabError error)
    {
        Debug.Log("Error: " + error);
    }
}
