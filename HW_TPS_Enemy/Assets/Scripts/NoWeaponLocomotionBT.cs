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
        pc = animator.GetComponent<PlayerController>();
        pc.moveSpeed = moveSpeed;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        pc.FrameMove();

        if (Input.GetKeyDown(KeyCode.E) && !pc.isEquipped && !animator.IsInTransition(0))  // 세번째 파라미터는 트랜지션구간에 실행안하게 하기위한 부분
        {
            //if(pc.GetNearestWeaponIn(radius: 1.5f, angle: 180f, weaponTag: "RightWeapon") != null)  // 공기줍기 방지
            GameObject weapon = pc.GetNearestWeaponIn(radius: 1.5f, angle: 180f, weaponTag: "RightWeapon");
            if (weapon == null)
            {
                Debug.Log("No weapon");
                return;
            }
            animator.SetInteger("HandlingWeaponId", weapon.GetComponent<WeaponType>().weaponId);
            animator.SetTrigger("PickupWeapon");
        }
        if (Input.GetKeyDown(KeyCode.X) && pc.isDisarmed && !pc.isEquipped && !animator.IsInTransition(0))
        {
            Transform weaponDisarmHolder = animator.GetComponent<PlayerController>().weaponDisarmHolder;
            Transform weapon = weaponDisarmHolder.GetChild(0);
            animator.SetInteger("HandlingWeaponId", weapon.GetComponent<WeaponType>().weaponId);
            animator.SetTrigger("Equip");
        }
        if (Input.GetKeyDown(KeyCode.C) && !animator.IsInTransition(0))
            animator.SetTrigger("ComboAttack");
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
