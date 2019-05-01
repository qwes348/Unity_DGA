using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    [HideInInspector]
    public float moveDir_X;

    [HideInInspector]
    public float moveDir_Z;

    float speed = 6f;
    Vector3 moveHorizontal;
    Vector3 moveVertical;

    [HideInInspector]
    public Vector3 totalMove;

    Rigidbody rb;

    bool isJumping;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        TryJump();

    }

    public void MovePlayer()
    {
        moveDir_X = Input.GetAxisRaw("Horizontal");
        moveDir_Z = Input.GetAxisRaw("Vertical");

        moveHorizontal = transform.right * moveDir_X;
        moveVertical = transform.forward * moveDir_Z;

        totalMove = (moveHorizontal + moveVertical).normalized * speed;
        rb.MovePosition(transform.position + totalMove * Time.deltaTime);

    }

    public void TryJump()
    {
        if (Input.GetKey(KeyCode.Space))
            StartCoroutine(Jump());
    }

    IEnumerator Jump()
    {
        if (!isJumping)
        {
            isJumping = true;
            rb.AddForce(Vector3.up * 300f);
            yield return new WaitForSeconds(1.4f);
            isJumping = false;
        }   
    }

}
