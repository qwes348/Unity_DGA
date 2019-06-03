using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Openable : MonoBehaviour
{
    Animator anim;
    bool isOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();    
    }

    public void ToggleDoor()
    {
        isOpen = !isOpen;  // 토글
        anim.SetBool("IsOpen", isOpen);  // 애니메이터에 bool스위치에 대입
    }
}
