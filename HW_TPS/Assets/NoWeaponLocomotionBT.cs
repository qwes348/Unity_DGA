using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoWeaponLocomotionBT : StateMachineBehaviour
{
    public float moveSpeed = 4f;
    PlayerController pc;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        pc = animator.GetComponent<PlayerController>();   // animator를 가지고있는 게임오브젝트의 PlayerController를 가져올수있다.
        pc.moveSpeed = moveSpeed;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        pc.FrameMove();
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(pc.GetNearestWeaponIn(radius: 1.5f, angle: 180f, weaponTag: "RightWeapon") != null)
                animator.SetTrigger("PickUpWeapon");
        }
        if(Input.GetKeyDown(KeyCode.Alpha1) && pc.weaponHolder.childCount != 0)
        {
            animator.SetTrigger("EquipAxe");
        }
        if (Input.GetMouseButtonDown(0) && pc.weaponHolder.childCount != 0)
        {
            animator.SetTrigger("OnBaldoAttack");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        pc.moveDirection = Vector3.zero;
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
