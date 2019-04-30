using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump_Pattern : MonoBehaviour
{
    bool isMoving;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

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
            isMoving = true;
            rb.AddForce(Vector3.up * 300f);   // jump
            yield return new WaitForSeconds(1.5f);

            rb.AddForce(Vector3.up * 300f);  // jump
            yield return new WaitForSeconds(1.5f);

            rb.maxAngularVelocity = 100f;
            rb.AddForce(Vector3.up * 500f);  // highJump
            rb.angularVelocity = Vector3.up * 100f;  // rotate
            rb.angularDrag = 2f;                     // break
        }
        yield return new WaitForSeconds(3f);
        rb.angularDrag = 0.05f;
        isMoving = false;
    }

    //private void OnCollisionStay(Collision collision)
    //{
    //    if (collision.transform.name == "Plane")
    //        isMoving = false;
    //}
    //private void OnCollisionExit(Collision collision)
    //{
    //    isMoving = true;
    //}
}
