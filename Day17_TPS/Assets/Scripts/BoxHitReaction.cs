﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ReactionType
{
    None = 0,
    Head,
    Body,
    Stomach,
    Bottom,
    Stun
}

public class BoxHitReaction : MonoBehaviour
{
    public GameObject hitFXPrefab;
    public GameObject stunFXPrefab;
    public Transform stunFXPos;

    Animator anim;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }
    
    public void Hurt(int damage, Vector3 hitPoint, Vector3 hitNormal, Vector3 hitDirection, ReactionType reactionType)
    {
        if(anim != null
            && !anim.GetCurrentAnimatorStateInfo(0).IsTag("Reaction")
            && !anim.GetCurrentAnimatorStateInfo(0).IsTag("Invincible")
            && !anim.IsInTransition(0))
        {
            anim.SetTrigger("Reaction");
            anim.SetInteger("ReactionType", (int)reactionType);

            if(reactionType == ReactionType.Stun)
            {
                GameObject stunFX = Instantiate(stunFXPrefab, stunFXPos.position, Quaternion.LookRotation(Vector3.up), stunFXPos);
                Destroy(stunFX, 3.2f);
            }
        }
        GetComponent<Health>().DecreaseHP(damage);    // HP감소 옮겨옴
        GameObject fx = Instantiate(hitFXPrefab, hitPoint, Quaternion.identity);   // 이펙트소환
        Destroy(fx, 1.5f);

        /*rb?.AddForce(hitDirection, ForceMode.VelocityChange);*/    // 넉백
        // 동일한 함수 위아래
        //if (rb != null)
        //    rb.velocity += hitDirection;

        // 다른함수! 과하지않은 넉백
        if (rb != null)
            rb.velocity = hitDirection;
    }
}
