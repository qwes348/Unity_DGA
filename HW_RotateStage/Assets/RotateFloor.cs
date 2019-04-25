using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Time.deltaTime == 1초/프레임레이트
// 프레임레이트 == 초당 프레임수
// http://maedoop.dothome.co.kr/641
// https://bluemeta.tistory.com/1

public class RotateFloor : MonoBehaviour
{
    public float angle;
    public float duration;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float frameDuration = Time.deltaTime / duration;
        transform.Rotate(Vector3.up, angle * frameDuration);
        print((int)Time.time);
    }
}
