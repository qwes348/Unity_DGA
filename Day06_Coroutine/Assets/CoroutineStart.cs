using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineStart : MonoBehaviour
{
    // Start Can be Coroutine!! but Update Can't

    // Start is called before the first frame update
    IEnumerator Start()
    {
        print("Start()");
        StartCoroutine(Todo());
        print("B");
        yield return null;
        print("D");
    }

    IEnumerator Todo()
    {
        print("A");
        yield return null;
        print("C");
    }

    // Runtime Error Script error (CoroutineStart): Update() can not be a coroutine.
    // Update is called once per frame
    //IEnumerator Update()
    //{
    //    yield return null;
    //}
}
