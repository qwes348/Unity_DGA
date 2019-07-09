using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMgr : MonoBehaviour
{
    static public SceneMgr instance;
    public event Action OnBeginLoad;
    public event Action OnLoadCompleted;
    public event Action<float> OnProgress;
    public string prevScene;

    bool isLoading = false;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public void LoadScene(string nextScene)
    {
        if(!isLoading)
            StartCoroutine(AsynchronousLoad(nextScene));
    }

    IEnumerator AsynchronousLoad(string scene)
    {
        isLoading = true;
        prevScene = SceneManager.GetActiveScene().name;
        OnBeginLoad?.Invoke();  // null이 아니면 실행

        yield return null;

        AsyncOperation ao = SceneManager.LoadSceneAsync(scene);  // 코루틴같은 async식 sceneLoad
        ao.allowSceneActivation = false;

        while(!ao.isDone)
        {
            //float progress = Mathf.Clamp01(ao.progress / 0.9f);
            //OnProgress?.Invoke(progress);

            int i = 0;
            while(i<=10)  // 로딩시간 고의 지연
            {
                float progress = Mathf.Clamp01(i / 10f);
                OnProgress?.Invoke(progress);
                yield return new WaitForSeconds(0.01f);
                i++;
            }

            // Loading Completed;
            if (ao.progress == 0.9f)  // ao.progress는 0.9f가 최댓값!!
            {
                ao.allowSceneActivation = true;
                isLoading = false;
                OnLoadCompleted?.Invoke();                
            }

            yield return null;
        }
    }
}
