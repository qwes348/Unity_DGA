using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection_Co : MonoBehaviour
{
    public float angle = 90f;
    public float duration = 1f;

    bool isRotating = false;
    float remainingAngle;
    float remainingDuration;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isRotating)
        {
            StartCoroutine(Selection());
        }
    }

    IEnumerator Selection()
    {
        if (!isRotating)
        {
            float y = transform.rotation.y;
            yield return null;
            if(y == transform.rotation.y)
            {
                isRotating = true;
                remainingAngle = angle;
                remainingDuration = duration;
            }

        }
        while (isRotating)
        {
            float anglePerFrame = (remainingAngle / remainingDuration) * Time.deltaTime; // 한프레임의 회전값
            if (remainingAngle < anglePerFrame)
            {
                anglePerFrame = remainingAngle;
                isRotating = false;
            }
            transform.Rotate(Vector3.up * anglePerFrame);

            remainingAngle -= anglePerFrame;
            remainingDuration -= Time.deltaTime;
            yield return null;

        }
    }
}

// StartRotate 코루틴과
// RotateStage 코루틴을 따로짜서
// Start코루틴이 RotateStage를 yield return하는게 더 좋은방법!!
