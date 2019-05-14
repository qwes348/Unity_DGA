using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakUp : MonoBehaviour
{
    public Texture[] cracks;
    public ParticleSystem fx;

    Renderer render;
    int numHits = 0;
    float lastHitTime;
    float hitTimeThreadhold = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<Renderer>();
        lastHitTime = Time.time;
    }

    //public IEnumerator Hit()
    //{
    //    //StopCoroutine(Heal());
    //    CancelInvoke("Heal");
    //    //StopAllCoroutines();
    //    yield return new WaitForSeconds(1f);
    //    numHits++;
    //    if (numHits < cracks.Length)
    //    {
    //        render.material.SetTexture("_DetailMask", cracks[numHits]);
    //        //yield return new WaitForSeconds(1f);
    //    }
    //    else
    //    {
    //        var clone = Instantiate(fx, transform.position, Camera.main.transform.rotation);
    //        Destroy(clone, 1f);    // 게임오브젝트 말고 컴포넌트도 지울수있음 => 파라미터를 this를 넣으면 게임오브젝트가 아닌 스크립트가 삭제됨
    //        Destroy(gameObject);
    //    }
    //    lastHitTime = Time.time;
    //    //StartCoroutine(Heal());
    //    Invoke("Heal", 2f);
    //}
    public void Hit()
    {
        StopCoroutine(Heal());
        if (Time.time > lastHitTime + hitTimeThreadhold)
        {
            numHits++;
            if (numHits < cracks.Length)
                render.material.SetTexture("_DetailMask", cracks[numHits]);
            else
            {
                var clone = Instantiate(fx, transform.position, Camera.main.transform.rotation);
                Destroy(clone, 1f);    // 게임오브젝트 말고 컴포넌트도 지울수있음 => 파라미터를 this를 넣으면 게임오브젝트가 아닌 스크립트가 삭제됨
                Destroy(gameObject);
            }
            lastHitTime = Time.time;
        }
        StartCoroutine(Heal());
    }

    IEnumerator Heal()
    {
        yield return new WaitForSeconds(2f);
        numHits = 0;
        render.material.SetTexture("_DetailMask", cracks[0]);
    }
}
