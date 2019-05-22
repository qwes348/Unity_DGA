using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHorizontal : StateMachineBehaviour, IHitBoxResponder
{
    public int damage = 5;
    HitBox hitBox;
    Dictionary<int, int> hitObjects;

    public void CollisionWith(Collider collider)
    {
        HurtBox hurtBox = collider.GetComponent<HurtBox>();
        //Debug.Log("Hit: " + collider.name);   

        int id = collider.transform.root.gameObject.GetInstanceID();
        if (!hitObjects.ContainsKey(id))
            hitObjects[id] = 1;
        else
        {
            hitObjects[id] += 1; 
            return;    // 다단히트를 결정짓는부분 return이 없으면 다단히트들어감
        }

        hurtBox.GetHitBy(damage);   // debugging
        //collider.GetComponentInParent<Health>().DecreaseHP(damage);
        Vector3 cameraTargetPosition = hitBox.transform.root.Find("CameraTarget").transform.position;
        RaycastHit hit;
        Vector3 hitPoint = collider.transform.position;
        Vector3 hitNormal = cameraTargetPosition - hitPoint;
        hitNormal = hitNormal.normalized;
        Vector3 hitDirection = -hitNormal;
        if(Physics.Raycast(cameraTargetPosition,
                            hitDirection,
                            out hit,
                            2f,
                            1 << LayerMask.NameToLayer("HurtBox"),  // bit 쉬프트연산
                            //1024, // == 위와같음
                            QueryTriggerInteraction.Collide))
        {
            hitPoint = hit.point;
            hitNormal = hit.normal;
            hitDirection = hitPoint - cameraTargetPosition;
            hitDirection = hitDirection.normalized;
        }

        Debug.Log("1Hit " + collider.name);
        Debug.DrawLine(cameraTargetPosition, hitPoint, Color.yellow, 2f);
        Debug.DrawLine(hitPoint, hitPoint + hitNormal, Color.magenta, 2f);
        Debug.DrawLine(hitPoint, hitPoint + hitDirection, Color.cyan, 2f);
        BoxHitReaction hr = collider.GetComponentInParent<BoxHitReaction>();
        hr?.Hurt(damage, hitPoint, hitNormal, hitDirection);
    }

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        hitBox = animator.GetComponent<PlayerController>().weaponHolder.GetComponentInChildren<HitBox>();
        hitBox.SetResponder(this);
        hitBox.StartCheckingCollision();
        hitObjects = new Dictionary<int, int>();
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(0.35f <= stateInfo.normalizedTime && stateInfo.normalizedTime <= 0.45f)  // 타격타이밍 맞추기
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
