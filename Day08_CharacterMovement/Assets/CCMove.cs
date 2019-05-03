using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCMove : MonoBehaviour
{
    public float moveSpeed = 8f;
    public float jumpHeight = 2f;
    public LayerMask groundMask;
    public Transform groundChecker;
    public float slideSpeed = 3f;
    Vector3 move;

    CharacterController cc;
    Vector3 velocity;
    [SerializeField]
    bool isGrounded = true;
    [SerializeField]
    bool onSlidingSlope = false;

    Vector3 hitNormal;
    Vector3 hitPoint;


    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundChecker.position, 0.5f, groundMask, QueryTriggerInteraction.Ignore);
        if (isGrounded && velocity.y < 0)
            velocity.y = 0f;

        move = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        move = move.normalized;
        cc.Move(move * moveSpeed * Time.deltaTime);
        // cc.Move == 한프레임당 변해야하는 양을넘겨서 이동해줌
        // rb.Moveposition == 한프레임후에 플레이어가 있어야될 위치를 받아 이동해줌
        if (move != Vector3.zero)
            transform.forward = move;

        if (Input.GetButtonDown("Jump"))
            velocity.y += Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y);  // 물리효과 직접구현 점프Velocity

        velocity.y += Physics.gravity.y * Time.deltaTime;  // 중력을 직접적용

        onSlidingSlope = Vector3.Angle(Vector3.up, hitNormal) > cc.slopeLimit;
        Vector3 slidieDirection = Vector3.zero;
        if(onSlidingSlope)
        {
            Vector3 c = Vector3.Cross(hitNormal, Vector3.up);
            slidieDirection = Vector3.Cross(hitNormal, c) * slideSpeed;
            Debug.DrawRay(hitPoint, slidieDirection, Color.magenta, 1f);
        }

        cc.Move((velocity + slidieDirection) * Time.deltaTime);
    }

    // Particle simulation
    // p(i+1) = p(i) + v(i+1) * dt
    // v(i+1) = v(i) + a(i+1) * dt
    // a(i+1) = F(i+1) /m    : F = m*a   =>  F = m*g

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(groundChecker.position, 0.5f);

    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        hitNormal = hit.normal;
        hitPoint = hit.point;

        var h = hit.gameObject.GetComponent<HealingPlatform>();
        if (h != null)
            GetComponent<HealOverTime>().Heal();

        // 숙제
        //float ascentAngle = hit.transform.rotation.eulerAngles.x;
        //if (ascentAngle >= 180)
        //    ascentAngle = Mathf.Abs(ascentAngle - 360);

        //if (ascentAngle >= 45)
        //{
        //    if (move == Vector3.zero)
        //    {                
        //        transform.Translate(new Vector3(0f, -1f, -1f) * Time.deltaTime, Space.World);
        //    }
        //}

    }
}
