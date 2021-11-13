using System;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;

public class PlayFabLogin : MonoBehaviour
{
    public Action<LoginResult> Logined;
    
    [SerializeField] private InputField _loginInput;
    [SerializeField] private InputField _passwordInput;

    [SerializeField] private Button _loginButton;

    [SerializeField] private GameObject _loginPanel;
    
    private void OnEnable()
    {
        _loginButton.onClick.AddListener(TryLogin);
    }

    private void OnDisable()
    {
        _loginButton.onClick.RemoveListener(TryLogin);
    }

    private void TryLogin()
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = _loginInput.text,
            Password = _passwordInput.text
        };
        
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnError);
    }

    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Successful login");
        Logined?.Invoke(result);
        _loginPanel.SetActive(false);
    }
    
    private void OnError(PlayFabError error)
    {
        Debug.Log("Error: " + error);
    }
}
