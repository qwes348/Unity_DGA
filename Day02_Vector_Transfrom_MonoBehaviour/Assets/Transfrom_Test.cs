using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transfrom_Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // transform이 계층구조(부모 자식관계)를 표현한다!!!
        print(transform.position);
        print(transform.rotation);
        print(transform.lossyScale);

        print(transform.forward);
        print(transform.right);
        print(transform.up);

        // transform = GetComponent<Transform>();
        print(transform.childCount == 3);
        print(transform.GetChild(0).name == "B");
        print(transform.GetChild(1).name == "C");
        print(transform.GetChild(0).parent.name == "A");
        print(transform.Find("D").name == "D");
        print(transform.Find("D/F").name == "F");
        print(transform.Find("D/F").root == transform); // transform == 부모(root)
        print(transform.Find("D/F").root.name == "A");
        print(transform.Find("D/F").root.name == transform.name);
        print(transform.Find("D/F").root.name == transform.gameObject.name);

        GetComponent<MeshRenderer>().material.color = Color.yellow;
        gameObject.GetComponent<MeshRenderer>().material.color = Color.yellow;
        transform.gameObject.GetComponent<MeshRenderer>().material.color = Color.yellow;
    }
}
