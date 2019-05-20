using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisarmSword : StateMachineBehaviour
{
    PlayerController pc;
    Transform weaponHolder;
    Transform disarmHolder;
    GameObject weapon;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        pc = animator.GetComponent<PlayerController>();
        weaponHolder = pc.weaponHolder;
        disarmHolder = pc.disarmHolder;
        weapon = weaponHolder.GetChild(0).gameObject;
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.normalizedTime > 0.4 && !animator.IsInTransition(0))
        {
            //Weapon.SetActive(false);
            weapon.transform.SetParent(disarmHolder);
            weapon.transform.localPosition = new Vector3(0f, 0.1f, 0f);
            weapon.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
        }
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
