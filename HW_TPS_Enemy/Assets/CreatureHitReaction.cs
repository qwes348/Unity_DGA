using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureHitReaction : MonoBehaviour
{
    public GameObject hitFXPrefab;

    Rigidbody rb;
    Animator anim;
    Health health;
    bool isDead = false;

    public bool isTracing = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        health = GetComponent<Health>();
    }

    public void Hurt(int damage, Vector3 hitPoint, Vector3 hitNormal, Vector3 hitDirection)
    {
        GetComponent<Health>().DecreaseHP(damage);    // HP감소 옮겨옴
        GameObject fx = Instantiate(hitFXPrefab, hitPoint, Quaternion.identity);   // 이펙트소환
        Destroy(fx, 1.5f);
        rb?.AddForce(hitDirection * 1f, ForceMode.VelocityChange);    // 넉백
        isTracing = true;
        anim.SetBool("isTracing", isTracing);

        if (health.currentHealth == 0 && !isDead)
        {
            isDead = true;
            anim.SetTrigger("isDie");
            Destroy(this.gameObject, 5f);
        }
    }
}
