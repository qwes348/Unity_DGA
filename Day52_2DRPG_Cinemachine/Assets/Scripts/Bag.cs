using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class Bag : MonoBehaviour
{
    [HideInInspector]
    public GameObject[] slots;
    bool isActive = false;

    void Start()
    {
        List<Slot> list = new List<Slot>(FindObjectsOfType<Slot>());
        slots = new GameObject[list.Count];

        foreach (var s in list)
        {
            slots[s.slotId] = s.gameObject;  // FindObjectsOfType으로는 순서가 보장되지않으므로 Slot스크립트에 slotId를줘서 순서를 맞춰준다.
        }
    }

    public void Show()
    {
        GetComponent<RectTransform>().DOMoveY(0, 0.5f).SetEase(Ease.OutCubic).OnComplete(() =>
        {
            isActive = true;
        });        
    }

    public void Hide()
    {
        GetComponent<RectTransform>().DOMoveY(-120, 0.5f).SetEase(Ease.InCubic).OnComplete(() =>
        {
            isActive = false;
        });        
    }

    public void OnOff()
    {
            if (isActive)
                Hide();
            else
                Show();
    }

    public void UpdateBag(Item[] items)
    {        
        int i = 0;
        foreach(var item in items)
        {
            if(item.itemData != null)
            {
                var s = slots[i];
                foreach(Transform child in s.transform)
                {
                    Destroy(child.gameObject);
                }
                var button = Instantiate(item.itemData.itemButtonPrefab, s.transform, false);
                //BounceAnim(button.transform);
            }
            i++;
        }
    }

    private void BounceAnim(Transform t)
    {
        t.DOScale(1.2f, 0.2f).OnComplete(() =>
        {
            t.localScale = Vector3.one;
        });
    }
}
