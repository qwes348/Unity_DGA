using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAttack : StateMachineBehaviour, IHitBoxResponder
{
    public int damage = 5;
    public bool enabledMultipleHits = false;

    HitBox hitBox;
    bool entered;  // 이펙트가 한번만 들어가게하기위한 변수

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
        hr?.Hurt(damage, hitPoint, hitNormal, hitDirection, ReactionType.Stun);
    }

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        hitBox = animator.GetComponent<MobController>().jumpAttackHitBox;
        hitBox.SetResponder(this);
        hitBox.enabledMultipleHit = this.enabledMultipleHits;
        hitBox.StartCheckingCollision();
        entered = false;
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (0.56f <= stateInfo.normalizedTime && stateInfo.normalizedTime <= 0.80f)  // 타격타이밍 맞추기
            hitBox.UpdateHitBox();
        if(!entered && stateInfo.normalizedTime >= 0.6f)
        {
            MobController mc = animator.GetComponent<MobController>();
            var fx = Instantiate(mc.jumpAttackFX, mc.transform.position, Quaternion.identity);
            Destroy(fx, 2f);

            AddFroceToEny(200f, hitBox.transform.position, 5f, 1f);

            CameraShake cs = Camera.main.GetComponent<CameraShake>();
            cs.enabled = true;
            cs.StartCoroutine(cs.Shake(0.1f, 0.4f));
            entered = !entered;
        }
    }

    private void AddFroceToEny(float power, Vector3 explosionPosition, float radius, float upwardsModifier) // 힘, 폭발지점, 거리원지름, 위쪽힘을줄지 안줄지
    {
        Collider[] colliders = Physics.OverlapSphere(explosionPosition, radius);
        foreach(var c in colliders)
        {
            Rigidbody rb = c.GetComponent<Rigidbody>();
            if(rb != null)
            {                
                rb.AddExplosionForce(power, explosionPosition, radius, upwardsModifier);
            }
        }
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
