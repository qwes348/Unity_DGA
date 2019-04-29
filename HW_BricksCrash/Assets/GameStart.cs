using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            rb.isKinematic = false;
            rb.transform.parent = null;
            rb.AddForce(Vector3.up * 500f);
            rb.AddForce(Vector3.right * 100f);
            enabled = false;
        }
    }
}
