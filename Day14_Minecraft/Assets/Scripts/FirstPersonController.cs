using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    public float moveSpeed = 8f;
    public float mouseSensitivity_X = 2f;
    public float mouseSensitivity_Y = 2f;
    public float jumpHeight = 2f;
    public Transform groundChecker;
    public LayerMask groundMask;

    Vector3 moveDirection = Vector3.zero;

    [SerializeField]
    bool isGrounded = false;
    RaycastHit hit;

    Transform cameraTransform;
    Rigidbody rb;
    float verticalLookRotation;
    Animator anim;
    float currentSpeed = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
        cameraTransform = GetComponentInChildren<Camera>().transform;
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;   // 마우스 포커스를 현재게임에만 맞추게한다
        Cursor.visible = false;
        anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        moveDirection = (new Vector3(h, 0, v)).normalized;
        moveDirection *= moveSpeed;        

        //print(Input.GetAxis("Mouse X"));
        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * mouseSensitivity_X);  // deltaTime 곱해줄 필요가 없음

        verticalLookRotation += Input.GetAxis("Mouse Y") * mouseSensitivity_Y;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90, 90);          // 카메라 각도제한
        cameraTransform.localEulerAngles = Vector3.left * verticalLookRotation;    // localEulerAngles 부모입장에서 회전???

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
        isGrounded = Physics.SphereCast(groundChecker.position, 0.5f, -transform.up, out hit, 0.2f, groundMask, QueryTriggerInteraction.Ignore);
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = Vector3.zero;
            rb.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
        }
        if (moveDirection != Vector3.zero || !isGrounded)
            currentSpeed = 1f;
        else
            currentSpeed = -1f;
        anim.SetFloat("Speed", currentSpeed);
    }

    private void FixedUpdate()
    {
        //TransformPoint() : position, rotation, scale
        //TransformDirection() : rotation only 벡터의 길이는 무시
        //TransformVector : rotation and scale only
        Vector3 move = transform.TransformDirection(moveDirection) * Time.fixedDeltaTime;  // transfromDirection
        rb.MovePosition(rb.position + move);
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.magenta;
    //    Gizmos.DrawSphere(groundChecker.position, 0.5f);
    //}
}
