using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StartStage : MonoBehaviour
{
    public SceneTransition sceneTransition;
    public AudioSource music;

    GameObject player;

    private void Awake()
    {
        player = GameFlow.instance.player;
        if (player == null)
        {
            GameFlow.instance.InstantiatePlayer();
            player = GameFlow.instance.player;
        }
    }

    void Start()
    {
        List<NextStage> entries = new List<NextStage>(FindObjectsOfType<NextStage>());
        var entry = entries.Find(o => o.nextStage == SceneMgr.instance.prevScene);     // predicate = return값이 bool인 Func()
        if(entry != null)
        {
            entry.sceneLoadEnabled = false;
            player.transform.position = entry.transform.position;
            StartCoroutine(sceneTransition.FadeOut());
            music?.DOFade(1f, 1f);
            player.GetComponent<Renderer>().enabled = false;
            entry.transform.GetChild(0).DOLocalMoveX(-1, 0.5f);
            entry.transform.GetChild(1).DOLocalMoveX(1, 0.5f).OnComplete(() =>
            {
                player.transform.localScale *= 0.5f;
                player.GetComponent<Renderer>().enabled = true;
                player.GetComponent<PlayerFSM>().lookAtHere = Vector3.down;
                player.transform.DOMoveY(-1.8f, 0.5f).SetRelative();
                player.transform.DOScale(1f, 0.5f).OnComplete(() =>
                {
                    entry.sceneLoadEnabled = true;
                    UIController.instance.bag.Show();
                    player.GetComponent<PlayerFSM>().controllable = true;
                    entry.transform.GetChild(0).DOLocalMoveX(-0.48f, 0.5f);
                    entry.transform.GetChild(1).DOLocalMoveX(0.48f, 0.5f);
                });
            });
        }
        else  // Title => Scene1 Case
        {
            player.GetComponent<PlayerFSM>().controllable = true;
            player.transform.position = transform.position;
            UIController.instance.bag.Show();
            StartCoroutine(sceneTransition.FadeOut());
        }                
    }
}
