using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInput : MonoBehaviour
{
    public GameObject sphere;

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetMouseButton(0)) // 0번은 왼클릭
        //{
        //    print("mousePoisition" + Input.mousePosition);
        //    print("Pressed left click");
        //}
        //if (Input.GetMouseButtonDown(0)) // 0번은 왼클릭 Down은 처음 한번누를때
        //{
        //    print("Pressed left click : Down");
        //}
        //if (Input.GetMouseButtonUp(0)) // 0번은 왼클릭 UP은 손을뗄때
        //{
        //    print("Pressed left click : Up");
        //}
        //if (Input.GetMouseButtonDown(1)) // 1번은 오클릭
        //{
        //    print("Pressed right click");
        //}
        //if (Input.GetMouseButtonDown(2)) // 2번은 휠클릭
        //{
        //    print("Pressed wheel click");
        //}

        if(Input.GetButtonDown("Fire1"))
        {
            print("Fire1");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, 100))
            {
                var o = Instantiate(sphere, hit.point, Quaternion.identity);  // sphere의 클론을 hit.point에 Quaternion은 기본값(회전시키지않고) 만든다
                Debug.DrawLine(ray.origin, hit.point, Color.red, 2f); // ray.origin = 시작점, hit.point = 끝점, 색상 red, 2f = 2초동안 유지
                Destroy(o, 2f);
            }
        }
    }
}
