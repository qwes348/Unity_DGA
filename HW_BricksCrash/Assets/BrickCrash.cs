using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickCrash : MonoBehaviour
{
    public GameObject debris;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.name == "Ball")
        {
            debris.transform.parent = null;
            debris.SetActive(true);
            Destroy(debris, 1f);
            Destroy(this.gameObject);
        }
    }

}
