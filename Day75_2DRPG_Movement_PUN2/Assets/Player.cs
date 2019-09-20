using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Player : MonoBehaviourPun, IPunObservable
{
    public float moveSpeed = 4f;

    Animator anim;
    float lastX, lastY;
    Vector3 heading;
    
    Vector3 networkPosition;
    
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!photonView.IsMine)
            return;

        heading = (new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0)).normalized;
        if(!anim.GetBool("Attack"))
            Move();

        if(Input.GetKeyDown(KeyCode.C) && !anim.GetBool("Attack"))
        {
            //Attack();
            photonView.RPC("AttackOnServer", RpcTarget.MasterClient);   // LocalClient에서 호출하지만 MasterClient에서만 실행(계산)이됨
            // 예를들어 A라는 플레이어가 어택을하면 서버상에 A플레이어의 컴포넌트에서 이 함수가 실행이됨
        }
        
        var rdr = GetComponent<SpriteRenderer>();
        rdr.color = Color.red;        
    }

    [PunRPC]
    void AttackOnServer(PhotonMessageInfo info)     // 이 함수안에 코드는 서버플레이어(마스터 클라이언트)만 실행됨
    {
        //int clientId = info.photonView.ViewID;
        //GameObject client = PhotonView.Find(clientId).gameObject;       // Attack키를 누른 플레이어의 정보를 구함

        //Vector3 pos = client.transform.position + Vector3.up * 0.2f;
        //Vector2 dir = client.GetComponent<Player>().GetHeadingDirection();    // clientId를 구할필요 없음 호출한 오브젝트를 주체로 실행이됨

        Vector3 pos = transform.position + Vector3.up * 0.2f;
        Vector2 dir = GetComponent<Player>().GetHeadingDirection();
        RaycastHit2D hit;

        hit = Physics2D.CircleCast(pos, 0.3f, dir, 1f, 1 << LayerMask.NameToLayer("HurtBox"));
        Debug.DrawRay(pos, dir, Color.white, 0.5f);
        if (hit.collider != null)
        {
            //hit.transform.GetComponentInParent<IDamageable>().TakeHit(10, hit.point, dir);
            int id = hit.transform.GetComponentInParent<PhotonView>().ViewID;       // 맞은 몬스터의 id를 구해옴
            hit.transform.GetComponentInParent<IDamageable>().TakeDamage(10);
            photonView.RPC("AttackEffectOnClient", RpcTarget.All, id, 10, hit.point, dir);
        }
        else
        {
            photonView.RPC("AttackEffectOnClient", RpcTarget.All);      // RpcTarget.All == 각 클라이언트의 자기자신object의 이함수를 똑같이 실행하게됨
        }
    }

    [PunRPC]
    void AttackEffectOnClient(int id, int damage, Vector2 point, Vector2 direction, PhotonMessageInfo info)     // 공격성공 판정합격
    {
        PhotonView.Find(id).GetComponent<IDamageable>().TakeHit(damage, point, direction);
        anim.SetBool("Attack", true);
    }

    [PunRPC]
    void AttackEffectOnClient(PhotonMessageInfo info)       // 공격실패 판정탈락
    {
        anim.SetBool("Attack", true);
    }

    public Vector2 GetHeadingDirection()
    {
        if (heading.magnitude == 0)
            return new Vector2(lastX, lastY);
        else
            return heading;
    }

    private void Attack()
    {
        Vector3 pos = transform.position + Vector3.up * 0.2f;
        Vector2 dir = GetHeadingDirection();
        RaycastHit2D hit;

        hit = Physics2D.CircleCast(pos, 0.3f, dir, 1f, 1 << LayerMask.NameToLayer("HurtBox"));
        Debug.DrawRay(pos, dir, Color.white, 0.5f);
        if(hit.collider != null)
        {
            hit.transform.GetComponentInParent<IDamageable>().TakeHit(10, hit.point, dir);
            hit.transform.GetComponentInParent<IDamageable>().TakeDamage(10);
        }
        anim.SetBool("Attack", true);
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
        if (heading.magnitude == 0)
        {
            anim.SetFloat("LastDirX", lastX);
            anim.SetFloat("LastDirY", lastY);
            anim.SetBool("Move", false);
        }
        else
        {
            lastX = heading.x;
            lastY = heading.y;
            anim.SetBool("Move", true);
        }
        if (heading != Vector3.zero)
        {
            anim.SetFloat("DirX", heading.x);
            anim.SetFloat("DirY", heading.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!photonView.IsMine)
            return;

        if(collision.CompareTag("Mob"))
        {
            //Attack();
            photonView.RPC("AttackOnServer", RpcTarget.MasterClient);
            print("Attack");
        }
    }    

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)    // 보낼때
        {            
            stream.SendNext(heading);
            stream.SendNext(transform.position);
            stream.SendNext(lastX);
            stream.SendNext(lastY);
        }
        else    // 받을때 꼭 보낸순서에 맞춰 받을것!
        {
            heading = (Vector3)stream.ReceiveNext();
            networkPosition = (Vector3)stream.ReceiveNext();
            lastX = (float)stream.ReceiveNext();
            lastY = (float)stream.ReceiveNext();

            float lag = Mathf.Abs((float)(PhotonNetwork.Time - info.SentServerTime));
            networkPosition += (heading * moveSpeed * lag);
        }
    }

    private void FixedUpdate()
    {
        if (!photonView.IsMine)
            transform.position = Vector3.Lerp(transform.position, networkPosition, 0.15f); 
    }
}
