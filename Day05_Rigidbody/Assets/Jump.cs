using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();  // GetComponent는 비용이 좀 있는 함수라서 업데이트에서 계속 호출하면 안좋다
    }
    private void FixedUpdate() // 물리 시뮬레이션은 FixedUpdate에 넣어야함
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * 400f);  // AddForce는 물리적으로 힘을 주는함수라서 오브젝트가 얼마나 이동할지 예측하기 힘들다
        }
    }
}
