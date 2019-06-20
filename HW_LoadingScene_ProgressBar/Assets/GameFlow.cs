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

    GameObject player;

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
        print("progress: " + progress);
        progressBar.GetComponent<Image>().fillAmount = progress;
    }

    private void OnBeginLoad()
    {
        print("OnBeginLoad");
        progressBar.parent.gameObject.SetActive(true);
    }

    private void OnLoadCompleted()
    {
        print("OnLoadCompleted");
        if(player == null)
        {            
            player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        }
        StartCoroutine(SpawnPlayer());
        progressBar.parent.gameObject.SetActive(false);
    }

    IEnumerator SpawnPlayer()
    {
        yield return null;
        GameObject portal = GameObject.FindWithTag("Portal");
        Vector3 spawnPoint = portal.transform.position + new Vector3(0f, -2f, 0f);
        //player = Instantiate(playerPrefab, spawnPoint, Quaternion.identity);
        player.transform.position = spawnPoint;
    }
}
