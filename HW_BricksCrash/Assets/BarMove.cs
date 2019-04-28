using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarMove : MonoBehaviour
{
    public float speed = 10f;
    public float h;

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Wall")
        {
            h = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        h *= speed * Time.deltaTime;

        transform.Translate(Vector3.right * h);
    }
}
