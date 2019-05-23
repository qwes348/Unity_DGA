using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TracePlayer : MonoBehaviour
{
    Rigidbody rb;    
    Vector3 targetPosition;
    CreatureHitReaction cr;
    RaycastHit hit;
    Animator anim;
    float moveSpeed = 2f;

    public bool isAttacking = false;
    public GameObject targetPlayer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cr = GetComponent<CreatureHitReaction>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cr.isTracing || isAttacking)
        {
            targetPosition = new Vector3(targetPlayer.transform.position.x, transform.position.y, targetPlayer.transform.position.z);
            transform.LookAt(targetPosition);
            if (Physics.Raycast(transform.position, transform.forward, out hit, 1.1f))
            {
                if (hit.collider.transform.root.gameObject.name == "Player")
                {
                    isAttacking = true;
                    cr.isTracing = false;
                    anim.SetBool("isTracing", cr.isTracing);
                    anim.SetBool("isAttacking", isAttacking);
                }
            }
            else
            {
                isAttacking = false;
                //cr.isTracing = true;
                anim.SetBool("isAttacking", isAttacking);
                //anim.SetBool("isTracing", cr.isTracing);
                return;
            }
        }
    }
    private void FixedUpdate()
    {
        if(cr.isTracing)
        {
            Vector3 move = transform.forward * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + move);
        }
    }

}
