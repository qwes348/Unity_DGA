using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartStage : MonoBehaviour
{
    void Start()
    {
        var player = GameFlow.instance.player;
        if(player == null)
        {
            GameFlow.instance.InstantiatePlayer();
            player = GameFlow.instance.player;
        }

        List<NextStage> entries = new List<NextStage>(FindObjectsOfType<NextStage>());
        var entry = entries.Find(o => o.nextStage == SceneMgr.instance.prevScene);     // predicate = return값이 bool인 Func()
        if(entry != null)
        {
            player.transform.position = entry.transform.position + Vector3.right * 2f;
        }
        else  // Title => Scene1 Case
        {
            player.transform.position = transform.position;
        }

        UIController.instance.bag.Show();
    }
}
