using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StartStage : MonoBehaviour
{
    public SceneTransition sceneTransition;
    public AudioSource music;
    
    void Start()
    {
        var player = GameFlow.instance.player;
        if (player == null)
        {
            GameFlow.instance.InstantiatePlayer();
            player = GameFlow.instance.player;
        }

        List<NextStage> entries = new List<NextStage>(FindObjectsOfType<NextStage>());  // Component List
        var entry = entries.Find(o => o.nextStage == SceneMgr.instance.prevScene);
        if (entry != null)
        {
            //player.transform.position = entry.transform.position + Vector3.up * -2f;            
            entry.transform.GetChild(0).DOLocalMoveX(-1, 0.5f);  // Coroutine 처럼 작동
            entry.transform.GetChild(1).DOLocalMoveX(1, 0.5f);
            StartCoroutine(OutPortal(entry));
        }
        else // Title -> Scene1
        {
            player.transform.position = transform.position;
        }

        StartCoroutine(sceneTransition.FadeOut());
        
        music?.DOFade(1f, 1f);
        player.GetComponent<PlayerFSM>().movable = true;
        //player.GetComponent<Renderer>().enabled = true;

        UIController.instance.bag.Show();
    }

    IEnumerator OutPortal(NextStage entry)
    {
        var player = GameFlow.instance.player;
        player.GetComponent<PlayerFSM>().lastX = 0f;
        player.GetComponent<PlayerFSM>().lastY = 0f;
        entry.GetComponent<BoxCollider2D>().enabled = false;

        yield return new WaitForSeconds(1f);

        player.GetComponent<Renderer>().enabled = true;       
        player.transform.position = entry.transform.position + Vector3.up * -1f;
        player.transform.DOMoveY(2f, 1f).SetRelative();
        player.GetComponent<Animator>().SetTrigger("OutPortal");
        player.transform.DOMoveY(-1f, 1f).SetRelative();
        entry.transform.GetChild(0).DOLocalMoveX(-0.5f, 0.5f);  // Coroutine 처럼 작동
        entry.transform.GetChild(1).DOLocalMoveX(0.5f, 0.5f);

        yield return new WaitForSeconds(2f);
        entry.GetComponent<BoxCollider2D>().enabled = true;

    }
}
