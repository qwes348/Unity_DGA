﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFSM : MonoBehaviour
{
    public float moveSpeed = 4f;

    public enum State { Entry = -1, Idle, Walk, Attack}
    public State state = State.Idle;
    public State prevState = State.Entry;

    Animator anim;
    float lastX, lastY;

    public void SetState(State state)
    {
        prevState = this.state;
        this.state = state;
    }
    private void Awake()
    {
        DontDestroyOnLoad(this);
        anim = GetComponent<Animator>();

    }

    
    IEnumerator Start()
    {
        yield return null;

        //GameObject portal = GameObject.FindWithTag("Portal");
        //Vector3 spawnPoint = portal.transform.position + new Vector3(0f, -2f, 0f);
        //transform.position = spawnPoint;
        while (true)
        {
            anim.SetInteger("State", (int)state);
            anim.SetInteger("PrevState", (int)prevState);

            switch (state)
            {
                case State.Idle:
                case State.Walk:
                    yield return StartCoroutine(Move());
                    break;
                case State.Attack:
                    yield return new WaitForSeconds(0.417f); // 애니메이션의 길이
                    SetState(State.Idle);
                    break;
                    // case문에 Default는 안쓰는게 권장사항이다                    
            }
        }
    }

    IEnumerator Move()
    {
        while(true)
        {
            anim.SetInteger("State", (int)state);
            anim.SetInteger("PrevState", (int)prevState);

            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");
            Vector3 heading = new Vector3(h, v, 0).normalized;
            Vector3 movement = heading * moveSpeed * Time.deltaTime;
            transform.position += movement;

            UpdateAnimation(heading);

            if (Input.GetKeyDown(KeyCode.K))
            {
                anim.SetTrigger("OnAttack");
                SetState(State.Attack);
                break;
            }

            yield return null;

        }
    }

    private void UpdateAnimation(Vector3 heading)
    {
        if (heading.x == 0 && heading.y == 0)
        {
            anim.SetFloat("LastDirX", lastX);
            anim.SetFloat("LastDirY", lastY);
            anim.SetBool("OnMove", false);
            SetState(State.Idle);
        }
        else
        {
            lastX = heading.x;
            lastY = heading.y;
            anim.SetBool("OnMove", true);
            SetState(State.Walk);
        }
        if (heading != Vector3.zero)
        {
            anim.SetFloat("DirX", heading.x);
            anim.SetFloat("DirY", heading.y);
        }
    }

}
