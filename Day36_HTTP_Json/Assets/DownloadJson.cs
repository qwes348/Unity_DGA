using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DownloadJson : MonoBehaviour
{
    // dummy json: https://jsonplaceholder.typicode.com/
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetJsonFromURL());
    }

    IEnumerator GetJsonFromURL()
    {
        string url = "https://jsonplaceholder.typicode.com/todos/1";

        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
            {
                print(www.error);
            }
            else
            {
                string json = www.downloadHandler.text;
                print(json);
            }
        }
    }
}
