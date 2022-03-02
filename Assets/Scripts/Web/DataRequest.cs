using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class DataRequest : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(Get());
    }

    private IEnumerator Get()
    {
        // var request = UnityWebRequest.Get("http://127.0.0.1:1024/api/getData?username=test&password=pass");
        var request = UnityWebRequest.Get("http://127.0.0.1:8080/api/register?username=test&password=pass");
        yield return request.SendWebRequest();
        
        Debug.Log(request.downloadHandler.text);
    }
}
