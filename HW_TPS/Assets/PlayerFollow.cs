using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    Movement movement;
    public Transform target;
    public float height;

    float distance = -10f;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0f, height, distance);
        transform.rotation = Quaternion.Euler(45f, 0f, 0f);
        movement = gameObject.GetComponentInParent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 newPos = new Vector3(target.position.x, height, target.position.z + distance);
        ////Vector3 newRot = new Vector3(45f, target.rotation.y, transform.localRotation.z);        
        //transform.position = newPos;
        //transform.rotation = Quaternion.Euler(newRot * movement.turnSpeed * Time.fixedDeltaTime);
    }
}
