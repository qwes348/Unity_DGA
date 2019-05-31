using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    public PlayerController pc;
    public GameObject axeTrailParticle;
    public GameObject axeGlowParticle;

    Animator playerAnim;
    OnAxeAttack axeAtk;

    // Start is called before the first frame update
    void Start()
    {
        playerAnim = pc.gameObject.GetComponent<Animator>();
        axeAtk = playerAnim.GetBehaviour<OnAxeAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pc.isEquipped)   // 장착했을때
            axeGlowParticle.SetActive(true);
        else if (pc.isDisarmed)     // 등에넣었을때
            axeGlowParticle.SetActive(false);
        else if (!pc.isEquipped && !pc.isDisarmed)  // 버렸을때
            axeGlowParticle.SetActive(false);

        if (axeAtk.isAttacking)     // 공격할때
            axeTrailParticle.SetActive(true);
        else
            axeTrailParticle.SetActive(false);
    }
}
