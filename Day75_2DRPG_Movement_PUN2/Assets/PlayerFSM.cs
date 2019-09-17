using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerFSM : MonoBehaviour, IPunObservable
{
    public float moveSpeed = 4f;

    public enum State { Entry = -1, Idle, Walk, Attack}
    public State state = State.Idle;
    public State prevState = State.Entry;

    Animator anim;
    float lastX, lastY;
    Vector3 heading;

    PhotonView pv;
    Vector3 networkPosition;

    public void SetState(State state)
    {
        prevState = this.state;
        this.state = state;
    }
    private void Awake()
    {
        anim = GetComponent<Animator>();
        pv = GetComponent<PhotonView>();
    }

    
    IEnumerator Start()
    {
        while (true)
        {
            if (pv.IsMine)
            {
                heading = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0).normalized;
                if (heading.magnitude > 0.01f)
                    SetState(State.Walk);
                else
                    SetState(State.Idle);
                if (Input.GetKeyDown(KeyCode.Z))
                    SetState(State.Attack);
            }

            switch (state)
            {
                case State.Idle:
                    Idle();
                    break;
                case State.Walk:
                    Move();
                    break;
                case State.Attack:
                    yield return StartCoroutine(Attack());
                    break;
                    // case문에 Default는 안쓰는게 권장사항이다                    
            }
            yield return null;
        }
    }

    IEnumerator Attack()
    {
        anim.SetTrigger("OnAttack");
        yield return new WaitForSeconds(0.417f); // 애니메이션의 길이
        SetState(State.Idle);
    }

    private void Idle()
    {
        UpdateAnimation(heading);
    }

    void Move()
    {       
        Vector3 movement = heading * moveSpeed * Time.deltaTime;
        transform.position += movement;

        UpdateAnimation(heading);
    }

    private void UpdateAnimation(Vector3 heading)
    {
        if (heading.x == 0 && heading.y == 0)
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

    private void Update()
    {
        if(pv.IsMine)
        {
            var rdr = GetComponent<SpriteRenderer>();
            rdr.color = Color.red;
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)    // 보낼때
        {
            stream.SendNext(state);
            stream.SendNext(heading);
            stream.SendNext(transform.position);
        }
        else    // 받을때 꼭 보낸순서에 맞춰 받을것!
        {
            state = (State)stream.ReceiveNext();
            heading = (Vector3)stream.ReceiveNext();
            networkPosition = (Vector3)stream.ReceiveNext();
        }
    }

    private void FixedUpdate()
    {
        if(!pv.IsMine)
            transform.position = Vector3.MoveTowards(transform.position, networkPosition, Time.fixedDeltaTime);  // 등속보간 != lerp
    }
}
