using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipingAttack : StateMachineBehaviour, IHitBoxResponder
{
    public int damage = 5;
    public bool enabledMultipleHits = false;    

    HitBox hitBox;    

    public void CollisionWith(Collider collider, HitBox hitbox)
    {
        HurtBox hurtBox = collider.GetComponent<HurtBox>();
        //Debug.Log("Hit: " + collider.name);   


        // hitpoint를 계산하는 부분
        hurtBox.GetHitBy(damage);   // debugging

        Vector3 from = hitbox.transform.position;
        Vector3 hitPoint;
        Vector3 hitNormal;
        Vector3 hitDirection;

        hitBox.GetContactInfo(from: from,
                               to: collider.ClosestPoint(from),  // from에서 collider와 가장가까운 충돌지점 반환
                               out hitPoint, out hitNormal, out hitDirection,
                               2f);

        BoxHitReaction hr = collider.GetComponentInParent<BoxHitReaction>();
        hr?.Hurt(damage, hitPoint, hitNormal, hitDirection);
    }

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        hitBox = animator.GetComponent<MobController>().leftHandHitBox;
        hitBox.SetResponder(this);
        hitBox.enabledMultipleHit = this.enabledMultipleHits;
        hitBox.StartCheckingCollision();        
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (0.51f <= stateInfo.normalizedTime && stateInfo.normalizedTime <= 0.62f)  // 타격타이밍 맞추기
            hitBox.UpdateHitBox();
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
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
