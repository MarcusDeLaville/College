using System;
using System.Collections;
using PlayFab.ClientModels;
using UnityEngine;
using Newtonsoft.Json;
using PlayFab;
using UnityEngine.UI;

public class LoginLogic : MonoBehaviour
{
    private LoginFeature _login;

    private void Awake()
    {
        _login = FindObjectOfType<LoginFeature>();
    }

    private void OnEnable()
    {
        _login.LoginScreenRefs.Login.onClick.AddListener(Login);
    }

    private void OnDisable()
    {
        _login.LoginScreenRefs.Login.onClick.RemoveListener(Login);
    }

    private void Login()
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = _login.LoginScreenRefs.LoginInput.text,
            Password = _login.LoginScreenRefs.PasswordInput.text
        }
        ;
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnError);
    }

    private void OnError(PlayFabError playFabError)
    {
        Debug.Log("Error" + playFabError.Error);
    }

    private void OnLoginSuccess(LoginResult loginResult)
    {
        Debug.Log("Logined" + loginResult.InfoResultPayload.PlayerProfile.PlayerId);
        _login.LoginScreenRefs.LoginScreen.SetActive(false);
        _login.LoginScreenRefs.ProfileScreen.SetActive(true);

        string name = null;
        name = loginResult.InfoResultPayload.PlayerProfile.DisplayName;
        _login.LoginScreenRefs.NameProfile.text = name;
        
        StartCoroutine(LoadAvatar(loginResult.InfoResultPayload.PlayerProfile.AvatarUrl, _login.LoginScreenRefs.IconProfile));
    }

    private IEnumerator LoadAvatar(string url, Image icon)
    {
        WWW wwwLoader = new WWW(url);

        yield return wwwLoader;
        icon.material.mainTexture = wwwLoader.texture;
    }
    
    
}
