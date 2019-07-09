using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    static Transform draggingItemButton;
    static Slot enteredSlot;
    static Slot beginSlot;
    // static으로 해놓은이유 = Draggable스크립트를 가진 slot들의 공유변수로 쓰기위해, static으로 해놓으면 저 변수는 하나만 존재하게된다

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(transform.childCount > 0)
        {
            beginSlot = GetComponent<Slot>();
            Item item = GameDataManager.instance.GetItem(beginSlot.slotId);
            if (item.itemData == null)
                return;
            draggingItemButton = transform.GetChild(0);
            draggingItemButton.GetComponent<Image>().raycastTarget = false;
            Transform canvas = GetComponentInParent<Canvas>().transform;

            draggingItemButton.SetParent(canvas, false);
            draggingItemButton.SetAsLastSibling();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(draggingItemButton != null)
        {
            draggingItemButton.position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(enteredSlot != null && draggingItemButton != null)       // Swap and Move case
        {
            if(enteredSlot.transform.childCount > 0)   // Swap
            {
                //print("Swap");
                Item item = GameDataManager.instance.GetItem(beginSlot.slotId);
                if (item.itemData == null)
                    return;
                Item item2 = GameDataManager.instance.GetItem(enteredSlot.slotId);
                if (item2.itemData == null)
                    return;
                Transform otherButton = enteredSlot.transform.GetChild(0);
                draggingItemButton.SetParent(enteredSlot.transform, false);
                draggingItemButton.localPosition = Vector3.zero;
                BounceAnim(draggingItemButton);
                otherButton.SetParent(beginSlot.transform, false);                
                BounceAnim(otherButton);
                int fromId = beginSlot.slotId;
                int toId = enteredSlot.slotId;
                GameDataManager.instance.SwapItem(fromId, toId, false);

            }
            else        // move
            {
                //print("move");
                Item item = GameDataManager.instance.GetItem(beginSlot.slotId);
                if (item.itemData == null)
                    return;
                draggingItemButton.SetParent(enteredSlot.transform, false);
                draggingItemButton.localPosition = Vector3.zero;
                BounceAnim(draggingItemButton.transform);
                int fromId = beginSlot.slotId;
                int toId = enteredSlot.slotId;
                GameDataManager.instance.MoveItem(from: fromId, to: toId, false);
            }
            draggingItemButton.GetComponent<Image>().raycastTarget = true;
        }
        else if(enteredSlot == null && draggingItemButton != null)      // drop case
        {
            // rollback
            //draggingItemButton.SetParent(transform, false);
            //draggingItemButton.localPosition = Vector3.zero;

            Item item = GameDataManager.instance.GetItem(beginSlot.slotId);
            if (item.itemData == null)
                return;
            draggingItemButton.GetComponent<Spawn>().SpawnItem();
            Destroy(draggingItemButton.gameObject);
            GameDataManager.instance.RemoveItemAt(beginSlot.slotId);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        enteredSlot = GetComponent<Slot>();
    }

    public void OnPointerExit(PointerEventData eventData)
    {        
        enteredSlot = null;
    }

    private void BounceAnim(Transform t)
    {
        t.DOScale(1.2f, 0.2f).OnComplete(() =>
        {
            t.localScale = Vector3.one;
        });
    }
}
