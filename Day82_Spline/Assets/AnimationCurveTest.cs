using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCurveTest : MonoBehaviour
{
    public AnimationCurve curve;

    Vector3 finalPosition;
    Vector3 initialiPosition;

    // Start is called before the first frame update
    void Start()
    {
        initialiPosition = transform.position;
        finalPosition = initialiPosition + Vector3.forward * 10;
        StartCoroutine(MoveObject());
    }

    IEnumerator MoveObject()
    {
        curve.postWrapMode = WrapMode.PingPong;
        yield return new WaitForSeconds(1);
        float i = 0;
        float rate = 1 / 2f;

        while(i < 4)
        {
            i += rate * Time.deltaTime;
            float eval = curve.Evaluate(i);
            print(eval);
            transform.position = Vector3.Lerp(initialiPosition, finalPosition, eval);
            yield return null;
        }
    }
}
