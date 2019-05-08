using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hold : MonoBehaviour
{
    public Transform handle;
    public GameObject currentGun;
    public Camera fpsCamera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            RaycastHit hit;
            if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, 1f))  // 총에서부터가 아니라 카메라에서 쏨
            {
                if (hit.transform.tag == "Holdable" && handle.childCount == 0)
                {
                    hit.rigidbody.isKinematic = true;
                    hit.collider.isTrigger = true;
                    hit.transform.parent = handle;
                    hit.transform.Translate(Vector3.zero);
                    currentGun.SetActive(false);
                }
            }
        }
    }
}
