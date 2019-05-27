using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobController : MonoBehaviour
{
    public Transform player;

    bool isDead = false;
    Health health;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        health = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = player.position - transform.position;
        float angle = Vector3.Angle(dir, transform.forward);
        anim.SetFloat("Angle", angle);
        anim.SetFloat("Distance", dir.magnitude);

        if(health.currentHealth == 0)
        {
            if (!isDead)
            {
                isDead = true;
                transform.GetComponent<Animator>().SetBool("isDead", true);
                Destroy(this.gameObject, 7f);
            }
        }
    }
}
