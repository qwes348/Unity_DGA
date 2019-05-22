using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHorizontal_HW : StateMachineBehaviour, IHitBoxResponder
{
    HitBox hitbox;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        hitbox = GameObject.Find("BattleAxe").transform.GetComponentInChildren<HitBox>();
        hitbox.SetResponder(this);
        hitbox.StartCheckingCollision();
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        hitbox.UpdateHitBox();
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        hitbox.StopCheckingCollision();
    }

    public void CollisionWith(Collider collider)
    {
        collider.transform.parent.GetComponent<Rigidbody>().AddForce(collider.transform.forward * 50f);
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
