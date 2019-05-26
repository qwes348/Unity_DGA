using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureAttack : StateMachineBehaviour, IHitBoxResponder
{
    public int damage = 5;
    public bool enabledMultipleHits = false;

    HitBox hitBox;
    Dictionary<int, int> hitObjects;

    public void CollisionWith(Collider collider, HitBox hitbox)
    {
        HurtBox hurtBox = collider.GetComponent<HurtBox>();
        //Debug.Log("Hit: " + collider.name);   


        // hitpoint를 계산하는 부분
        hurtBox.GetHitBy(damage);   // debugging

        Vector3 targetHitChecker = hitBox.transform.root.Find("TargetHitChecker").transform.position;
        Vector3 hitPoint;
        Vector3 hitNormal;
        Vector3 hitDirection;

        hitBox.GetContactInfo(from: targetHitChecker,
                       to: collider.transform.root.transform.position,
                       out hitPoint, out hitNormal, out hitDirection,
                       2f);

        BoxHitReaction hr = collider.GetComponentInParent<BoxHitReaction>();
        CreatureHitReaction cr = collider.GetComponentInParent<CreatureHitReaction>();
        hr?.Hurt(damage, hitPoint, hitNormal, hitDirection);
        cr?.Hurt(damage, hitPoint, hitNormal, hitDirection);
    }

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        hitBox = animator.gameObject.GetComponentInChildren<HitBox>();
        hitBox.SetResponder(this);
        hitBox.enabledMultipleHit = this.enabledMultipleHits;
        hitBox.StartCheckingCollision();
        hitObjects = new Dictionary<int, int>();
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (0.48f <= stateInfo.normalizedTime && stateInfo.normalizedTime <= 0.52f)  // 타격타이밍 맞추기
            hitBox.UpdateHitBox();
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        hitBox.StopCheckingCollision();
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
