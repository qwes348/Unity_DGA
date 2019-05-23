using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxHitReaction : MonoBehaviour
{
    public GameObject hitFXPrefab;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    public void Hurt(int damage, Vector3 hitPoint, Vector3 hitNormal, Vector3 hitDirection)
    {
        GetComponent<Health>().DecreaseHP(damage);    // HP감소 옮겨옴
        GameObject fx = Instantiate(hitFXPrefab, hitPoint, Quaternion.identity);   // 이펙트소환
        Destroy(fx, 1.5f);

        /*rb?.AddForce(hitDirection, ForceMode.VelocityChange);*/    // 넉백
        // 동일한 함수 위아래
        //if (rb != null)
        //    rb.velocity += hitDirection;

        // 다른함수! 과하지않은 넉백
        if (rb != null)
            rb.velocity = hitDirection;
    }
}
