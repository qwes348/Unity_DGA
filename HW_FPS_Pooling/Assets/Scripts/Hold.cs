using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hold : MonoBehaviour
{
    public Transform holdPoint;
    public GameObject currentGun;
    public Camera fpsCamera;
    Rigidbody rb;
    Collider coll;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F) && holdPoint.childCount == 0)   // 잡을때
        {
            RaycastHit hit;            
            if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, 1f)) 
            {
                if (hit.transform.tag == "Holdable")
                {
                    rb = hit.rigidbody;
                    coll = hit.collider;
                    rb.isKinematic = true;
                    //coll.isTrigger = true;                    
                    //hit.rigidbody.isKinematic = true;
                    //hit.collider.isTrigger = true;
                    hit.transform.parent = holdPoint;
                    hit.transform.localPosition = Vector3.zero;
                    hit.transform.rotation = new Quaternion(0, 0, 0, 0);
                    currentGun.SetActive(false);
                }
            }
        }
        else if(Input.GetKeyDown(KeyCode.F) && holdPoint.childCount == 1)  // 던질때
        {
            var item = holdPoint.GetChild(0);
            rb.isKinematic = false;
            //coll.isTrigger = false;
            //item.GetComponent<Rigidbody>().isKinematic = false;
            //item.GetComponent<Collider>().isTrigger = false;

            item.GetComponent<Rigidbody>().AddForce(item.transform.forward * 500f);
            item.transform.parent = null;
            currentGun.SetActive(true);
        }
    }
}
