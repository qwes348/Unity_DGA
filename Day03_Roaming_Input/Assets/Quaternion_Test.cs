using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quaternion_Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.eulerAngles = new Vector3(0f, 50f, 0f);  // 세개모두 각도를변경할땐 Z축이 우선 X-Y순서
        // 단점: 돌리다보면 자유를 잃어버린다 어디가 원래앞인지 모르게됨(?)
        print(Mathf.Approximately(transform.eulerAngles.y, 50f));

        transform.rotation = Quaternion.Euler(0f, 100f, 0f);   // 새로 100도 돌림 자주쓰는함수 Quaternion.Euler
        print(Mathf.Approximately(transform.eulerAngles.y, 100f));

        transform.Rotate(Vector3.up * 90f);
        print(Mathf.Approximately(transform.eulerAngles.y, 190f));  // -170(인스펙터값)

        transform.rotation *= Quaternion.AngleAxis(90f, Vector3.up);
        print(Mathf.Approximately(transform.eulerAngles.y, 280f));  // -80(인스펙터값)

        transform.rotation = Quaternion.identity;                   // 회전이 되지않은상태로 초기화
        print(Mathf.Approximately(transform.eulerAngles.x, 0f));
        print(Mathf.Approximately(transform.eulerAngles.y, 0f));
        print(Mathf.Approximately(transform.eulerAngles.z, 0f));

        transform.rotation = Quaternion.FromToRotation(Vector3.up, transform.forward);   // Vector3.up을 forward각도로 맞춤
        print(Mathf.Approximately(transform.eulerAngles.x, 90f));

        transform.rotation = Quaternion.LookRotation(Vector3.right, Vector3.up);  // 첫번째파람은 forward를 바꿀 vector, 두번째파람은 up을 바꿀 Vector
        print(Mathf.Approximately(transform.eulerAngles.y, 90f));
        transform.rotation = Quaternion.LookRotation(Vector3.right);
        print(Mathf.Approximately(transform.eulerAngles.y, 90f));

        Quaternion q1 = Quaternion.Euler(0f, -45f, 0f);
        Quaternion q2 = Quaternion.Euler(0f, 45f, 0f);
        print(Mathf.Approximately(Quaternion.Angle(q1, q2), 90f));

        //transform.rotation *= Quaternion.Euler(0f, 100f, 0f);   // 누적 100도 돌림 자주쓰는함수 Quaternion.Euler(Quaternion의 누적연산은 곱셈!!)
        //print(Mathf.Approximately(transform.eulerAngles.y, 200f));
    }

}
