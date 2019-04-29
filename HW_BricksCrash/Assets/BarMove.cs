using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarMove : MonoBehaviour
{
    private float speed = 25f;
    private float h;
    Rigidbody rb;


    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;
        //h *= speed * Time.deltaTime;

        //transform.Translate(Vector3.right * h);
        //transform.Translate(h, 0, 0);
        rb = GetComponent<Rigidbody>();
        rb.MovePosition(transform.position + transform.right * h);

    }
}
