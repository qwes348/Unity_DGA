using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DOTweenTest : MonoBehaviour
{
    bool isRotating = false;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(1f);
        //transform.DOJump(new Vector3(3, 0, 0), 3f, 1, 1f);
        //transform.DOMoveX(5f, 2f);
        /*transform.DOMoveX(5f, 2f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);*/  // 등속Ver.
        //transform.DOMoveX(5f, 2f).SetLoops(-1, LoopType.Restart).SetEase(Ease.OutElastic);
        //GetComponent<MeshRenderer>().material.DOColor(Color.magenta, 1f);
        //GetComponent<MeshRenderer>().material.DOColor(Color.magenta, 1f).SetLoops(-1, LoopType.Yoyo);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Jump") && !isRotating)
        {
            isRotating = true;
            transform.DORotate(transform.up * 90f, 5f).SetEase(Ease.OutElastic).SetRelative().OnComplete(() =>  // OnComplete 람다로 회전이 끝나면 isRotating 실팽
            {
                isRotating = false;
            });
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            DOTween.KillAll(transform);
        }
    }
}
