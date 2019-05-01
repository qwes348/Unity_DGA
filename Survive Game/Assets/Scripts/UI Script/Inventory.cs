using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public static bool inventoryActivated = false;

    // 필요한 컴포넌트
    [SerializeField]
    private GameObject go_Inventory_Base;
    [SerializeField]
    private GameObject go_SlotParent;

    // 슬롯들
    private Slot[] slots;

    // Start is called before the first frame update
    void Start()
    {
        slots = go_SlotParent.GetComponentsInChildren<Slot>();
    }

    // Update is called once per frame
    void Update()
    {
        TryOpenInventory();
    }

    private void TryOpenInventory()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            inventoryActivated = !inventoryActivated;

            if(inventoryActivated)
                OpenInventory();
            else
                CloseInventory();
        }
    }

    private void OpenInventory()
    {
        go_Inventory_Base.SetActive(true);
    }

    private void CloseInventory()
    {
        go_Inventory_Base.SetActive(false);
    }

    public void AcquireItem(Item _item, int _count =1)
    {
        if(Item.ItemType.Equipment != _item.itemType)
        {
            for(int i=0; i < slots.Length; i++)
            {
                if(slots[i].item != null)
                {
                    if(slots[i].item.itemName == _item.itemName)
                    {
                        slots[i].SetSlotCount(_count);
                        return;
                    }
                }
                
            }
        }
        
       
        for(int i=0; i < slots.Length; i++)
        {
            if(slots[i].item == null)
            {
                slots[i].AddItem(_item, _count);
                return;
            }
        }
    }
}
