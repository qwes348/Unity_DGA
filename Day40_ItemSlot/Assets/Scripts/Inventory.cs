using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [HideInInspector]
    public GameObject[] slots;

    // Start is called before the first frame update
    void Start()
    {
        List<Slot> list = new List<Slot>(FindObjectsOfType<Slot>());
        slots = new GameObject[list.Count];
        
        foreach(var s in list)
        {
            slots[s.slotId] = s.gameObject;  // FindObjectsOfType으로는 순서가 보장되지않으므로 Slot스크립트에 slotId를줘서 순서를 맞춰준다.
        }
    }

}
