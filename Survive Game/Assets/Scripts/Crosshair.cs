using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{

    [SerializeField]
    private Animator animator;

    // 크로스헤어 상태에 따른 정확도
    private float gunAccracy;

    // 크로스헤어 비활성화를 위한 부모 객체
    [SerializeField]
    private GameObject go_CrosshairHUD;

    // 필요한 컴포넌트
    [SerializeField]
    private GunCtrl theGunController;

    public void WalkingAnimation(bool _flag)
    {
        WeaponManager.currentWeaponAnim.SetBool("Walk", _flag);
        animator.SetBool("Walking", _flag);
    }

        public void RunningAnimation(bool _flag)
    {
        WeaponManager.currentWeaponAnim.SetBool("Run", _flag);
        animator.SetBool("Running", _flag);
    }

       public void JumpingAnimation(bool _flag)
    {
        animator.SetBool("Running", _flag);
    }
   
    public void CrouchingAnimation(bool _flag)
    {
        animator.SetBool("Crouching", _flag);
    }

     public void FineSightAnimation(bool _flag)
    {
        animator.SetBool("Finesight", _flag);
    }

    public void FireAnimation()
    {
        if(animator.GetBool("Walking"))
            animator.SetTrigger("Walk_Fire");
        else if(animator.GetBool("Crouching"))
            animator.SetTrigger("Crouch_Fire");
        else
        {
            animator.SetTrigger("Idle_Fire");
        }
    }  

    public float GetAccuracy()
    {
         if(animator.GetBool("Walking"))
            gunAccracy = 0.06f;
        else if(animator.GetBool("Crouching"))
            gunAccracy = 0.015f;
        else if(theGunController.GetFineSightMode())
            gunAccracy = 0.001f;
        else
            gunAccracy = 0.035f;

        return gunAccracy;
    }

}
