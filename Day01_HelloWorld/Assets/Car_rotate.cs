using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_rotate : MonoBehaviour
{
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        float rotationSpeed = 240f;
        if (Input.GetKey(KeyCode.W))
            transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime);
        if (Input.GetKey(KeyCode.S))
            transform.Rotate(-Vector3.right * rotationSpeed * Time.deltaTime);

        /*float v = Input.GetAxisRaw("Vertical");
        v *= speed * Time.deltaTime;

        transform.Translate(Vector3.forward * v); */
    }
}
