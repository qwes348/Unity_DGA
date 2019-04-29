using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        print("Start()");
        StartCoroutine(Todo());
        print("B");
    }

    IEnumerator Todo()
    {
        print("A");
        yield return null; // 한프레임 기다림 (다음 프레임까지 제어권 넘김)
        print("C");
        yield return new WaitForSeconds(Time.deltaTime);  //  yield return null과 비슷함(아예 똑같지는 않음)
        print("D");
        yield return StartCoroutine(NewTodo()); // 제어권이 Todo로 넘어감 (yield return으로 호출했기때문에 NewTodo가 완전히 끝날때까지 기다림)
        print("G");
    }

    IEnumerator NewTodo()
    {
        print("E");
        yield return new WaitForSeconds(1f);  // yield return으로 호출됐기 때문에 대기를해도 제어권을 넘겨주지않음
        print("F");        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
