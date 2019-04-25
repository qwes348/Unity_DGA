using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D)) // bool 타입
            transform.Rotate(Vector3.up * 5f);
    }
}


// 좌우 누르면 회전
// 앞뒤누르면 회전하며 전후진 구현하기