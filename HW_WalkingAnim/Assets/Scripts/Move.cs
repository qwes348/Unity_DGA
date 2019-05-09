using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float moveSpeed = 8f;
    public float jumpHeight = 2f;
    public LayerMask groundMask;
    public Transform groundChecker;
    public Animator anim;

    Rigidbody rb;
    Vector3 moveDirection = Vector3.zero;
    [SerializeField]
    bool isGrounded = false;
    RaycastHit hit;
    float currentSpeed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // 1:
        //Ray ray = new Ray(transform.position, -transform.up);
        //RaycastHit hit;
        //if (Physics.Raycast(ray, out hit, 1f + 0.1f)) // player의 길이 + 0.1f만큼 레이를쏨
        //    isGrounded = true;
        //else
        //    isGrounded = false;
        //Debug.DrawRay(ray.origin, ray.direction, Color.magenta, 0.2f);

        // 2:
        //isGrounded = Physics.CheckSphere(groundChecker.position, 0.5f, groundMask, QueryTriggerInteraction.Ignore);

        // 3:
        isGrounded = Physics.SphereCast(groundChecker.position, 0.5f, -transform.up, out hit, 0.2f, groundMask, QueryTriggerInteraction.Ignore);
        // 이방법은 groundChecker가 이미 땅에닿아있으면 체크를하지않는다 그러므로 Checker를 땅에서 조금 떨어뜨려놔야 한다
        // 장점은 2번방법과 달리 hit정보를 받아오기때문에 hit정보를 다양하게 쓸수있다

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = Vector3.zero;
            rb.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);  // 정확한 높이로 점프시키기            
        }

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        moveDirection = (new Vector3(h, 0, v)).normalized;
        moveDirection *= moveSpeed;
        transform.LookAt(transform.position + moveDirection);
        if (moveDirection != Vector3.zero || !isGrounded)
            currentSpeed = 1f;
        else
            currentSpeed = -1f;
        anim.SetFloat("Speed", currentSpeed);
    }

    private void FixedUpdate()
    {
        Vector3 move = moveDirection * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + move);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(groundChecker.position, 0.5f);

    }

}
