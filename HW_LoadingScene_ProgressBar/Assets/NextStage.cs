using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextStage : MonoBehaviour
{
    public string nextStage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            transform.GetChild(1).transform.Rotate(Vector3.up * 180f);
            transform.GetChild(1).transform.position += new Vector3(-0.5f, 0f, 0f);
            transform.GetChild(2).transform.Rotate(Vector3.up * 180f);
            transform.GetChild(2).transform.position += new Vector3(0.5f, 0f, 0f);
            SceneMgr.instance.LoadScene(nextStage);            
        }
    }
}
