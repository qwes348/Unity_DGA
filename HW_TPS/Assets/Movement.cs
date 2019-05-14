using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 5f;

    Animator anim;
    Vector3 moveDirection = Vector3.zero;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        moveDirection = (new Vector3(0, 0, v));
        if (moveDirection == Vector3.zero)
            anim.SetFloat("Speed", -1f);
        else
            anim.SetFloat("Speed", 1f);

        transform.LookAt(transform.position + moveDirection);
        moveDirection *= moveSpeed;

    }

    private void FixedUpdate()
    {
        Vector3 move = moveDirection * Time.fixedDeltaTime;
        rb.MovePosition(transform.position + move);
    }
}
