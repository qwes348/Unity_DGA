using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PostController : MonoBehaviour
{
    public List<GameObject> posts;
    public string imageUrl;

    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject p in posts)
        {
            var info = p.transform.GetChild(0);
            var photo = p.transform.GetChild(1);
            var comment = p.transform.GetChild(2);

            GameDataManager.Instance?.AddPost(profile: info.GetChild(0).GetComponent<Image>(),
                                              friendName: info.GetChild(1).GetChild(0).GetComponent<Text>().text,
                                              location: info.GetChild(1).GetChild(1).GetComponent<Text>().text,
                                              photo: photo.GetComponent<Image>(),
                                              likes: comment.GetChild(0).GetComponent<Text>().text,
                                              description: comment.GetChild(1).GetComponent<Text>().text);
        }

        StartCoroutine(ChangePhoto(imageUrl));

    }

    IEnumerator ChangePhoto(string imageUrl)
    {
        // regacy
        WWW www = new WWW(imageUrl);

        yield return www;

        posts[0].transform.GetChild(1).GetComponent<Image>().sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0f, 0f));

        // Error ㅠㅠ
        //UnityWebRequest www = UnityWebRequestTexture.GetTexture(imageUrl);
        //if (www.isNetworkError || www.isHttpError)
        //{
        //    Debug.Log(www.error);
        //}
        //else
        //{
        //    Texture texture = ((DownloadHandlerTexture)www.downloadHandler).texture;            
        //    posts[0].transform.GetChild(1).GetComponent<Image>().sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0f, 0f));
        //}


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
