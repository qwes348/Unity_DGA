using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFSM : MonoBehaviour
{
    public float moveSpeed = 4f;

    public enum State { Entry = -1, Idle, Walk, Attack}
    public State state = State.Idle;
    public State prevState = State.Entry;
    public bool controllable = true;
    public Vector3 lookAtHere;

    Animator anim;
    float lastX, lastY;

    FloatingJoystick joystick;
    JoystickButton attackButton;

    public void SetState(State state)
    {
        prevState = this.state;
        this.state = state;
    }
    private void Awake()
    {
        DontDestroyOnLoad(this);
        anim = GetComponent<Animator>();
    }

    
    IEnumerator Start()
    {
        // Turn off FadeInPanel's RaycastTartget !!
        joystick = FindObjectOfType<FloatingJoystick>();
        attackButton = FindObjectOfType<JoystickButton>();

        while (true)
        {
            anim.SetInteger("State", (int)state);
            anim.SetInteger("PrevState", (int)prevState);

            switch (state)
            {
                case State.Idle:
                case State.Walk:
                    yield return StartCoroutine(Move());
                    break;
                case State.Attack:
                    yield return new WaitForSeconds(0.417f); // 애니메이션의 길이
                    SetState(State.Idle);
                    break;
                    // case문에 Default는 안쓰는게 권장사항이다                    
            }
        }
    }

    IEnumerator Move()
    {
        while(true)
        {
            anim.SetInteger("State", (int)state);
            anim.SetInteger("PrevState", (int)prevState);            

            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");
            Vector3 heading;
            if (controllable)
            {
                heading = new Vector3(h + joystick.Horizontal, v + joystick.Vertical, 0).normalized;
                Vector3 movement = heading * moveSpeed * Time.deltaTime;
                transform.position += movement;
            }
            else
            {
                heading = lookAtHere;
            }

            UpdateAnimation(heading);
            
            if (Input.GetKeyDown(KeyCode.C) || attackButton.pressed)
            {
                anim.SetTrigger("OnAttack");
                SetState(State.Attack);
                break;
            }

            if(Input.GetKeyDown(KeyCode.I))
            {
                UIController.instance.bag.OnOff();
            }

            yield return null;

        }
    }

    private void UpdateAnimation(Vector3 heading)
    {
        if (heading.x == 0 && heading.y == 0)
        {
            anim.SetFloat("LastDirX", lastX);
            anim.SetFloat("LastDirY", lastY);
            anim.SetBool("OnMove", false);
            SetState(State.Idle);
        }
        else
        {
            lastX = heading.x;
            lastY = heading.y;
            anim.SetBool("OnMove", true);
            SetState(State.Walk);
        }
        if (heading != Vector3.zero)
        {
            anim.SetFloat("DirX", heading.x);
            anim.SetFloat("DirY", heading.y);
        }
    }

}
