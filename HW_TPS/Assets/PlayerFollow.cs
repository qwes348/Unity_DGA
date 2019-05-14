using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    public Transform target;
    public float height;

    float distance = -10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = new Vector3(target.position.x, height, target.position.z - distance);
        transform.LookAt(target);
        transform.position = newPos;
    }
}
