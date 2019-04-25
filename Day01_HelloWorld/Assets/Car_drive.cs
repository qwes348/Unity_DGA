using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_drive : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        float rotationSpeed = 240f;

        if (Input.GetKey(KeyCode.D)) // bool 타입
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        if (Input.GetKey(KeyCode.A))
            transform.Rotate(-Vector3.up * rotationSpeed * Time.deltaTime);

        float speed = 10f; 

        float v = Input.GetAxisRaw("Vertical");
        v *= speed * Time.deltaTime;

        transform.Translate(Vector3.forward * v);
    }
}
