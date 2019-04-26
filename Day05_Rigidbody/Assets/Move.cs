using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float speed = 10f;
    public float rotationSpeed = 120f;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // [-1 ,0 , 1] (정확히 셋중 하나만 나옴)
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");

        v *= speed * Time.fixedDeltaTime;
        h *= rotationSpeed * Time.fixedDeltaTime;

        //transform.Translate(0, 0, v);     // 로컬좌표 기준
        //transform.Rotate(0, h, 0);
        rb.MovePosition(transform.position + transform.forward * v);     // 월드좌표 기준
        rb.MoveRotation(transform.rotation * Quaternion.Euler(Vector3.up * h));
    }
}
