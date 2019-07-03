using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 4f;
    [HideInInspector]
    public Vector3 heading = Vector3.zero;

    Animator anim;
    float lastX, lastY;
    Attack attack;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        attack = gameObject.transform.GetComponent<Attack>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        heading = new Vector3(h, v, 0).normalized;
        Vector3 movement = heading * moveSpeed * Time.deltaTime;

        if (attack.isAttacking == false)
        {
            transform.position += movement;

            UpdateAnimation(heading);
        }
    }

    private void UpdateAnimation(Vector3 heading)
    {
        if(heading.x == 0 && heading.y == 0)
        {
            anim.SetFloat("LastDirX", lastX);
            anim.SetFloat("LastDirY", lastY);
            anim.SetBool("OnMove", false);
        }
        else
        {
            lastX = heading.x;
            lastY = heading.y;
            anim.SetBool("OnMove", true);
        }
        if (heading != Vector3.zero)
        {
            anim.SetFloat("DirX", heading.x);
            anim.SetFloat("DirY", heading.y);
        }
    }
}
