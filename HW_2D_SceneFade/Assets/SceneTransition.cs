using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public virtual IEnumerator FadeIn()
    {
        //float timer = 1f;
        anim.SetTrigger("FadeIn");
        //GameObject player = GameFlow.instance.player;
        //player.GetComponent<Animator>().SetTrigger("IntoPortal");

        //while(timer >= 0)
        //{
        //    player.transform.position += Vector3.up * 0.1f;
        //    timer -= Time.deltaTime;
        //    yield return null;
        //}
        yield return new WaitForSeconds(1f);
    }

    public virtual IEnumerator FadeOut()
    {
        anim.SetTrigger("FadeOut");
        yield return new WaitForSeconds(1f);
    }
}
