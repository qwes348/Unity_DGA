using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    private void OnEnable()  // 삭제가아닌 껏다켰다 하는식이기 때문에 여기서 초기화해줘야함
        // 대표적으로는 총알의 Rigidbody의 Velocity값을 여기서 초기화시켜줘야함
    {
        print("OnEnable");
    }

    private void OnDisable()
    {
        print("OnDisable");
    }
}
