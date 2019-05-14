using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    Animator anim;
    //Movement movement;
    [HideInInspector]
    public bool isAttacking;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        //movement = gameObject.transform.GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            StartCoroutine(Attacking());
        }
    }

    IEnumerator Attacking()
    {
        anim.SetTrigger("Attack");
        isAttacking = true;
        yield return new WaitForSeconds(0.5f);
        isAttacking = false;
    }
}
