using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayRoaming : MonoBehaviour
{
    public GameObject wayPoints;
    public Transform wayTransfrom;
    int i = 0;


    // Start is called before the first frame update
    void Start()
    {
        wayPoints = GameObject.Find("Waypoints");
        wayTransfrom = wayPoints.transform;

    }

    // Update is called once per frame
    void Update()
    {
        float speed = 5f;
        if (i >= wayTransfrom.childCount)
            i = 0;

        Vector3 direction = wayTransfrom.GetChild(i).position - transform.position;
        wayTransfrom.GetChild(i).GetComponent<MeshRenderer>().material.color = Color.magenta;

        if (direction.magnitude > 0.5)
        {
            direction.Normalize();
            direction[1] = 0;

            transform.Translate(direction * speed * Time.deltaTime, Space.World);

        }
        else
        {
            wayTransfrom.GetChild(i).GetComponent<MeshRenderer>().material.color = Color.white;
            i++;
        }

    }
}
