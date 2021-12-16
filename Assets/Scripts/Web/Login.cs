using System;
using System.Collections;
using System.Net;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Networking;
using Web;

public class Login : MonoBehaviour
{
    [SerializeField] private string _userName;
    [SerializeField] private string _password;

    [SerializeField] private string _url;

    private void Start()
    {
        // StartCoroutine(GetRequest());
    }

    // [ContextMenu("TryRequest")]
    [Button]
    public void TryLogin()
    {
        StartCoroutine(TryingLogin());
    }

    private IEnumerator TryingLogin()
    {
        WWWForm form = new WWWForm();
        form.AddField("Login", _userName);
        form.AddField("Password", _password);

        using (UnityWebRequest request = UnityWebRequest.Post("https://n92834.hostru08.fornex.host/college/login.php", form))
        {
            yield return request.Send();

            // if (request.result != UnityWebRequest.Result.Success)
            // {
            //     Debug.Log("Ошибка: " + request.error);
            // }
            // {
            //     Debug.Log("SERVER: " + request.downloadHandler.text);
            // }

            if(request.isNetworkError) 
            {
                Debug.Log(request.error);
            }
            else
            {
                string DATA = "";
                using (UnityWebRequest request2 = UnityWebRequest.Put("https://n92834.hostru08.fornex.host/login.php", DATA))
                {
                    yield return request2.SendWebRequest();

                    if (request2.isNetworkError || request2.isHttpError)
                    {
                        Debug.Log("Ошибка: " + request2.error);
                    }
                    {
                        Debug.Log("SERVER: " + DATA);
                    }
                }
            }
        }
    }
    
    private IEnumerator GetText()
    {
        using (UnityWebRequest request = UnityWebRequest.Get("https://n92834.hostru08.fornex.host/GetData.php"))
        {
            yield return request.Send();

            if (request.isNetworkError || request.isHttpError)
            {
                Debug.Log("Ошибка: " + request.error);
            }
            {
                Debug.Log("SERVER: " + request.downloadHandler.text);
            }
        }
    }

    private IEnumerator GetRequest()
    {
        UnityWebRequest request = UnityWebRequest.Get(this._url);

        yield return request.SendWebRequest();

        Response response = JsonUtility.FromJson<Response>(request.downloadHandler.text);
    }
    
    
}
