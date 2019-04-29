using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump_Pattern : MonoBehaviour
{
    bool isMoving;
    Rigidbody rb;
    int jumpCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !isMoving)
        {
            StartCoroutine(JumpPattern());
        }
    }

    IEnumerator JumpPattern()
    {
        if (!isMoving)
        {
            rb.AddForce(Vector3.up * 300f);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.name == "Plane")
            isMoving = false;
    }
    private void OnCollisionExit(Collision collision)
    {
        isMoving = true;
    }
}
