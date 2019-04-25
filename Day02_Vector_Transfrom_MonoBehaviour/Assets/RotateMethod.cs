using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateMethod : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * 30 * Time.deltaTime);
        transform.Rotate(0, Time.deltaTime, 0);
        transform.Rotate(Vector3.up, 30 * Time.deltaTime);
    }
}
