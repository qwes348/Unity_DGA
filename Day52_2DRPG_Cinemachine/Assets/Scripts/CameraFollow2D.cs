using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    public float FollowSpeed = 2f;
    public Transform target;

    private void LateUpdate()
    {
        if(target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player")?.transform;
        }
        if (target == null)
            return;

        Vector3 newPosition = target.position;
        newPosition.z = -10;
        transform.position = Vector3.Lerp(transform.position, newPosition, FollowSpeed * Time.deltaTime);  // 가까워질수록 느려짐
    }
}
