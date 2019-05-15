using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 7f;
    public float turnSpeed = 100f;

    Animator anim;
    Vector3 moveDirection = Vector3.zero;
    Vector3 newRot = Vector3.zero;

    bool mouse_R = false;
    bool isMoving = false;
    float v, h;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        v = Input.GetAxisRaw("Vertical");
        h = Input.GetAxisRaw("Horizontal");
        
        if(Input.GetMouseButton(1))
        {
            anim.SetBool("Walk_F", true);
            mouse_R = true;
            transform.Translate(new Vector3(h, 0, 0) * moveSpeed * Time.deltaTime, Space.Self);
        }
        if(Input.GetMouseButtonUp(1))
        {
            mouse_R = false;
            anim.SetBool("Walk_F", false);            
        }

        WalkAnim();
        if (!isMoving && !mouse_R)
            TurnAnim();
    }

    public void TurnAnim()
    {
        

        if (h == 1)  
        {
            anim.SetTrigger("Turn_R");
            newRot = rb.rotation.eulerAngles;
            newRot.y += turnSpeed * Time.fixedDeltaTime;
        }
        else if(h == -1) 
        {
            anim.SetTrigger("Turn_L");
            newRot = rb.rotation.eulerAngles;
            newRot.y -= turnSpeed * Time.fixedDeltaTime;
        }
        //if (newRot.y >= 180f)
        //    newRot.y -= 360f;
        //newRot.y = Mathf.Clamp(newRot.y, -90f, 90f);
    }

    public void WalkAnim()
    {
                
        moveDirection = (new Vector3(0, 0, v));
        if (v == 0)  // 정지
        {
            isMoving = false;
            anim.SetBool("Walk_F", false);
            anim.SetBool("Walk_B", false);
        }
        else if (v == 1)  // 앞걸음
        {
            isMoving = true;
            anim.SetBool("Walk_F", true);
        }
        else  // 뒷걸음
        {
            isMoving = true;
            anim.SetBool("Walk_B", true);
        }

        moveDirection *= moveSpeed;
    }

    private void FixedUpdate()
    {
        Vector3 move = moveDirection * Time.fixedDeltaTime;
        //rb.MovePosition(rb.position + move);      // 항상 누르는 방향을 바라보는것이 아니기때문에 작동안함
        transform.Translate(moveDirection * Time.fixedDeltaTime, Space.Self);
        
        if(h != 0)
            rb.MoveRotation(Quaternion.Euler(newRot));
        //rb.rotation = Quaternion.Slerp(rb.rotation, Quaternion.Euler(newRot), turnSpeed*Time.fixedDeltaTime);
    }
}
