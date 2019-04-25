using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveControl : MonoBehaviour
{   
    // Update is called once per frame
    void Update()
    {
        float speed = 10f;
        float h = Input.GetAxisRaw("Horizontal"); // 좌우이동(-1, 0, 1)
        float v = Input.GetAxisRaw("Vertical");   // 상하이동(-1, 0, 1)

        // 1:
        //h *= speed * Time.deltaTime; // deltaTime 이 없으면 한프레임당 10f씩 이동해버림
        //v *= speed * Time.deltaTime;  // Time.deltaTime == 1/60 ,정확히는 이전프레임의 렌더링 시간

        //transform.Translate(Vector3.right * h); // Vector3.right == (1, 0, 0)
        //transform.Translate(Vector3.forward * v); // Vector3.forward == (0, 0, 1)
        //대각선은 길이가 루트2가 나오는 문제가 있음

        // 2:
        Vector3 dir = new Vector3(h, 0, v);
        dir = dir.normalized; // 전체길이를 1로만들어줌
        transform.Translate(dir * speed * Time.deltaTime);
    }
}
