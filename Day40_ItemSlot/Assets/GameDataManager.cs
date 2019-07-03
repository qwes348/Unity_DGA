﻿using System;
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

    public int GetTimeStamp()
    {
        return timeStamp;
    }

    public ItemData[] GetItems()
    {
        return items;
    }

    internal void AddItemAt(int i, ItemData itemData)
    {
        items[i] = itemData;
        UpdateTimeStamp();
    }

    public void RemoveItemAt(int i)
    {
        items[i] = null;
        UpdateTimeStamp();
    }
}
