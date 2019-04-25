using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoBehaviour_Test : MonoBehaviour
{
    public Transform[] waypoints;
    public int speed = 1000;

    private void Awake()
    {
        print("AWake()");
    }
    // Start is called before the first frame update
    void Start()
    {
        print("Start()");
        print(name == "SomeTest");
        print(name == gameObject.name);
        print(transform == GetComponent<Transform>());
        print(transform == gameObject.GetComponent<Transform>());
        print(GetComponent<MonoBehaviour>() == this);

    }

    // Update is called once per frame
    void Update()
    {
        print("Update()");
    }

    private void FixedUpdate()
    {
        print("Fixed()");
    }

    private void LateUpdate()
    {
        print("LateUpdate()");        
    }

}
