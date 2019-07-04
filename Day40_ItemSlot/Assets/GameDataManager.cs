using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    [SerializeField]
    ItemData[] items;

    int timeStamp = 0;

    static public GameDataManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public int FindEmptySlot()
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == null)
                return i;
        }
        return -1;
    }

    private void Start()
    {
        UpdateTimeStamp();
    }

    void UpdateTimeStamp()
    {
        timeStamp++;
        if(timeStamp <= 0)
        {
            timeStamp = 1;
        }
    }

    public ItemData GetItem(int slotId)
    {
        return items[slotId];
    }

    public int GetTimeStamp()
    {
        return timeStamp;
    }

    public ItemData[] GetItems()
    {
        return items;
    }

    internal void AddItemAt(int i, ItemData itemData, bool redraw)
    {
        items[i] = itemData;
        if(redraw)
            UpdateTimeStamp();
    }

    public void RemoveItemAt(int i)
    {
        items[i] = null;
        UpdateTimeStamp();
    }

    public void MoveItem(int from, int to, bool redraw)
    {
        if (from == to)
            return;
        items[to] = items[from];
        items[from] = null;
        if (redraw)
            UpdateTimeStamp();
    }

    public void SwapItem(int fromId, int toId, bool redraw)
    {
        ItemData a = items[fromId];
        items[fromId] = items[toId];
        items[toId] = a;
        if (redraw)
            UpdateTimeStamp();
    }
}
