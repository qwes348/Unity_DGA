using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Vector3 v = new Vector3(1f, 1f, 1f);
        Vector3 u = new Vector3(1f, 1f, 1f);
        print(v == u);
        print(2 * v == new Vector3(2f, 2f, 2f));
        v = new Vector3(1f, 0f, 0f);
        print(v == Vector3.right);
        print(v.magnitude == 1f);
        print(Vector3.one.magnitude); // == 루트3
        print(v.ToString() == "1.0, 0.0, 0.0");
        print(v);
        print(Vector3.Distance(Vector3.zero, u) == u.magnitude);  // 두벡터값의 차이의 magnitude 이부분에서는 선보다 좌표로 인식
        print(Vector3.Angle(Vector3.right, Vector3.up) == 90f);  // 여기서는 벡터를 선으로 인식
        print(Vector3.Angle(Vector3.right, Vector3.forward) == 90f);
        print((2 * Vector3.up).normalized == Vector3.up);
        print((2 * Vector3.up) / (2 * Vector3.up).magnitude == Vector3.up);
        print((2 * Vector3.up) / (2 * Vector3.up).magnitude == (2*Vector3.up).normalized);
        print(Vector3.Cross(Vector3.right, Vector3.up) == Vector3.forward);
        print(Vector3.Cross(Vector3.up, Vector3.right) == Vector3.back);
        print(Vector3.Dot(Vector3.up, Vector3.up) == 1f);  // A.magnitude * B.magnitude * cos세타
        print(Vector3.Dot(Vector3.right, Vector3.up) == 0f);

        transform.position = new Vector3(5f, 0, 0);  // 좌표로 인식
    }

}
