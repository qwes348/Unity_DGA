using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    MeshRenderer renderer;
    private void Start()
    {
        renderer = GetComponent<MeshRenderer>();
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(FadeIn());
        }
    }

    IEnumerator FadeIn()  // 코루틴 사용으로 애니메이션 효과를 줄수있음
    {
        for(float f = 1f; f >= 0f; f-=0.01f)
        {
            Color c = renderer.material.color;
            c.a = f;
            renderer.material.color = c;
            yield return null;  // 한프레임 대기
        }
    }

}
