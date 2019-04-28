using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickCrash2 : MonoBehaviour
{
    //public GameObject debris;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Brick")
        {
            //debris.transform.parent = null;
            //debris.SetActive(true);
            //Destroy(debris, 1f);
            Destroy(collision.gameObject);
        }
    }

}