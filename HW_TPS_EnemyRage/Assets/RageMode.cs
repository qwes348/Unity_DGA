using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RageMode : MonoBehaviour
{
    public bool isRageMode = false;

    public bool endRagemode = false;
    Health health;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<Health>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health.currentHealth <= 50 && !isRageMode && !endRagemode)
        {
            isRageMode = true;
            anim.SetTrigger("OnRage");
            Invoke("EndRageMode", 30f);
        }
    }

    void EndRageMode()
    {
        endRagemode = true;
        isRageMode = false;
    }
}
