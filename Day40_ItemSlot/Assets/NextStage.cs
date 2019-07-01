using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NextStage : MonoBehaviour
{
    public string nextStage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            transform.GetChild(0).DOLocalMoveX(-1, 1);    // 내부적으로 코루틴으로 되어있음 async함
            transform.GetChild(1).DOLocalMoveX(1, 1);
            SceneMgr.instance.LoadScene(nextStage);            
        }
    }
}
