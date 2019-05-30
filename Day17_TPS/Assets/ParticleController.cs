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
        if (pc.isEquipped)
            axeGlowParticle.SetActive(true);
        else if (pc.isDisarmed)
            axeGlowParticle.SetActive(false);
        else if (!pc.isEquipped && !pc.isDisarmed)
            axeGlowParticle.SetActive(false);

        if (axeAtk.isAttacking)
            axeTrailParticle.SetActive(true);
        else
            axeTrailParticle.SetActive(false);
    }
}
