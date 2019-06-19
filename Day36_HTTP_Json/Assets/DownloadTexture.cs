using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DownloadTexture : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetTextureFromURL());
    }

    IEnumerator GetTextureFromURL()
    {
        // http :80 , https :443 기본포트
        string url = "https://avatars3.githubusercontent.com:443/u/48681306?s=460&v=4";

        // way 1
        using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(url))  
        {
            yield return uwr.SendWebRequest();
            if (uwr.isNetworkError || uwr.isHttpError)
            {
                print(uwr.error);
            }
            else
            {
                var texture = DownloadHandlerTexture.GetContent(uwr);
                GetComponent<Renderer>().material.mainTexture = texture;
            }
        }

        // way 2
        //{
        //    UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(url); 
        //    try
        //    {
        //        yield return uwr.SendWebRequest();
        //        if (uwr.isNetworkError || uwr.isHttpError)
        //        {
        //            print(uwr.error);
        //        }
        //        else
        //        {
        //            var texture = DownloadHandlerTexture.GetContent(uwr);
        //            GetComponent<Renderer>().material.mainTexture = texture;
        //        }
        //    }
        //    finally
        //    {
        //        if (uwr != null)
        //            uwr.Dispose();   // 메모리에서 제거
        //        // 바로 제거해주는 이유는 OS측에서 중요한 자원이기 때문에 안쓸거라면 바로바로 반납해줘야한다
        //    }
        //}

        // Way 3 == legacy
        //using (WWW www = new WWW(url))   
        //{
        //    yield return www;
        //    GetComponent<Renderer>().material.mainTexture = www.texture;
        //}
    }
}
