using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MobAwake : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Debug.Log("Awake Enter");
        //Debug.Log(stateInfo.IsName("Base Layer.Awake"));        // sateInfo는 현재애니메이션과 상관없이 그냥 이 스크립트가 있는 스테이트
        //Debug.Log(animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Idle"));  // Idle이 아직 안끝났고 Awake는 거의 시작전이기때문
        //Debug.Log(animator.GetNextAnimatorStateInfo(0).IsName("Base Layer.Awake"));     // Idle기준 다음스테이트는 Awake
        animator.GetComponent<NavMeshAgent>().isStopped = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    //Debug.Log("Awake Update");
    //    //Debug.Log(stateInfo.IsName("Base Layer.Awake"));
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    //Debug.Log("Awake Exit");
    //    //Debug.Log(stateInfo.IsName("Base Layer.Awake"));
    //    //Debug.Log(animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Chase"));   // 이미 Chase가 상당히 시작했기때문에
    //    //Debug.Log(animator.GetNextAnimatorStateInfo(0).IsName(""));
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
