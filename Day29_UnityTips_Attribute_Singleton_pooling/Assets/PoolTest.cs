using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZObjectPools;

public class PoolTest : MonoBehaviour
{
    public EZObjectPool shellPool;

    GameObject shell;

    // Start is called before the first frame update
    void Start()
    {
        if(shellPool.TryGetNextObject(Vector3.zero, Quaternion.identity, out shell))
        {
            print(shellPool.AvailableObjectCount() == 9); // pool에 남은 오브젝트 수
            print(shellPool.ActiveObjectCount() == 1);  // pool에서 나온 오브젝트 수
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            print(shellPool.AvailableObjectCount() == 9); // pool에 남은 오브젝트 수
            print(shellPool.ActiveObjectCount() == 1);  // pool에서 나온 오브젝트 수
            shell.SetActive(false);  // pool에 반납
            print(shellPool.AvailableObjectCount() == 10); // pool에 남은 오브젝트 수
            print(shellPool.ActiveObjectCount() == 0);  // pool에서 나온 오브젝트 수
        }
    }
}
