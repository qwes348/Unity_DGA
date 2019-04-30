using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineStop : MonoBehaviour
{
    // 1:
    //IEnumerator Start()
    //{
    //    yield return StartCoroutine("Todo", 2f);  // 코루틴 함수이름을 문자열로받을수있음
    //    // 일반함수도 invoke를 이용하면 문자열로 호출가능함
    //}

    //IEnumerator Todo(float someParam)
    //{
    //    while (true)
    //    {
    //        print("someParam: " + someParam);
    //        yield return new WaitForSeconds(1f);
    //    }
    //}

    //private void Update()
    //{
    //    if(Input.GetKeyDown(KeyCode.Space))
    //    {
    //        StopCoroutine("Todo");   // 루프돌고있는 특정 코루틴을 정지
    //        // 문자열을 param으로 넣을려면 시작할때도 문자열로 시작했어야 한다
    //        // Start와 Stop은 같은 형식의 param이 들어와야한다
    //    }
    //}

    // 2:
    //IEnumerator co;
    //void Start()
    //{
    //    co = Todo(2f, "String");  // IEnumerator 변수 co에 코루틴을 담음
    //    StartCoroutine(co);  // 코루틴변수 co 실행

    //}

    //IEnumerator Todo(float someParam, string str)
    //{
    //    while (true)
    //    {
    //        print("someParam: " + someParam + str);
    //        yield return new WaitForSeconds(0.1f);
    //    }
    //}

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        StopCoroutine(co);  // 변수co 스탑           
    //    }
    //}

    // 3:
    Coroutine co, co2; // 코루틴타입 변수 co
    void Start()
    {
        co = StartCoroutine(Todo(2f, "String")); // 코루틴타입 변수 co에 startcoroutine을 바로 담으면서 실행
        co2 = StartCoroutine(Todo(3f, "String"));

    }

    IEnumerator Todo(float someParam, string str)
    {
        while (true)
        {
            print("someParam: " + someParam + str);
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //StopCoroutine(co);  // 변수co 스탑           
            StopAllCoroutines();
        }
    }
}
