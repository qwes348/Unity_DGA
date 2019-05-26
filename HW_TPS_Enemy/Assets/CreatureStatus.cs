using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureStatus : MonoBehaviour
{
    Animator anim;
    Health health;

    // Start is called before the first frame update
    void Start()
    {
        anim = transform.GetComponent<Animator>();
        health = transform.GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        if(health.currentHealth == 0)
        {
            StartCoroutine(Dead());
        }
    }

    IEnumerator Dead()
    {
        anim.SetTrigger("isDie");
        yield return new WaitForSeconds(4f);
        Destroy(this.gameObject);
    }
}
