using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ActivateCamera : MonoBehaviour
{
    public float distance = 5.5f;
    CinemachineVirtualCamera vcam;

    // Start is called before the first frame update
    void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        if(vcam != null && vcam.Follow != null && GameFlow.instance.player != null)
        {
            var bb = vcam.Follow.GetComponent<CinemachineTargetGroup>().BoundingBox;
            print(bb.extents + ", " + bb.extents.magnitude);
            if (bb.extents.magnitude >= distance)
                vcam.Priority = 9;
            else
                vcam.Priority = 11;      // 우선순위가 "높을"수록 우선적임
        }
    }
}
