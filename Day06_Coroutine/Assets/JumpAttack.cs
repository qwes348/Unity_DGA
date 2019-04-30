using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAttack : MonoBehaviour
{
    Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(TripleJumpAttack());
        }
    }

    IEnumerator TripleJumpAttack()
    {
        Jump(3f);
        yield return new WaitForSeconds(1.8f);
        Jump(3f);
        yield return new WaitForSeconds(1.8f);
        yield return WheelWindJump(5f);
    }

    IEnumerator WheelWindJump(float height)
    {
        Jump(height);
        rb.maxAngularVelocity = 100;
        rb.angularVelocity = Vector3.up * 30f;
        //rb.angularDrag = 1f;
        //yield return new WaitForSeconds(0.5f);
        
        yield return new WaitForSeconds(2f);
        //rb.angularVelocity = Vector3.zero;
        rb.angularDrag = 0.05f;

    }

    private void Jump(float height)
    {
        Vector3 v = rb.velocity;
        v.y = Mathf.Sqrt(-2.0f * Physics.gravity.y * height);   // 정확히 height까지 점프하게만드는 Velocity값 외워야함!!
        rb.velocity = v;
        //rb.AddForce(v, ForceMode.VelocityChange); // 바로윗줄과 같은뜻
    }
}
