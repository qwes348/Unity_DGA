using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupObject : MonoBehaviour
{
    public Transform holder;

    Camera fpsCamera;

    // Start is called before the first frame update
    void Start()
    {
        fpsCamera = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            RaycastHit hit;
            if(Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, 4f))
            {
                print(hit.transform.name);
                if(hit.transform.tag == "Holdable")
                    Pickup(hit.transform);
            }
        }
        if (Input.GetMouseButtonDown(1))
            ThrowItem();
    }

    private void ThrowItem()
    {
        if(holder.childCount == 1)
        {
            Transform item = holder.GetChild(0);
            item.SetParent(null);
            item.GetComponent<Rigidbody>().isKinematic = false;
            item.GetComponent<Rigidbody>().AddForce(fpsCamera.transform.forward * 700f);
        }
    }

    private void Pickup(Transform item)
    {
        if(holder.childCount == 0)
        {
            item.GetComponent<Rigidbody>().isKinematic = true;
            //item.parent = holder;
            item.SetParent(holder);  // 이게 더 권장됨
            //item.transform.position = holder.transform.position;
            item.transform.localPosition = Vector3.zero;
            item.rotation = new Quaternion(0, 0, 0, 0);
        }
    }
}
