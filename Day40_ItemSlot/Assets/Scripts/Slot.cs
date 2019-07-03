using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public int slotId;

    public void Drop()
    {
        foreach(Transform child in transform)
        {
            child.GetComponent<Spawn>().SpawnItem();
            Destroy(child.gameObject);
            GameDataManager.instance.RemoveItemAt(slotId);
        }
    }
}
