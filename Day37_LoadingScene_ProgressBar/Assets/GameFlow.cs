using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameFlow : MonoBehaviour
{
    public GameObject playerPrefab;
    public RectTransform progressBar;

    static public GameFlow instance;

    public GameObject player;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SceneMgr.instance.OnBeginLoad += OnBeginLoad;
        SceneMgr.instance.OnLoadCompleted += OnLoadCompleted;
        SceneMgr.instance.OnProgress += OnProgress;
        SceneMgr.instance.LoadScene("Scene1");
    }

    private void OnProgress(float progress)
    {        
        progressBar.GetComponent<Image>().fillAmount = progress;
    }

    private void OnBeginLoad()
    {        
        progressBar.parent.gameObject.SetActive(true);
    }

    private void OnLoadCompleted()
    {        
        progressBar.parent.gameObject.SetActive(false);
    }

    public void InstantiatePlayer()
    {
        if (player == null)
        {
            player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        }
    }
}
