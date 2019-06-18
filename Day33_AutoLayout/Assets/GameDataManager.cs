using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatData
{
    public string Message { get; set; }
    public bool IsAlignLeft { get; set; }
}
public class GameDataManager : Singleton<GameDataManager>
{
    protected GameDataManager() { }

    List<ChatData> messages = new List<ChatData>();
    int timeStamp = 0;

    public void AddMessage(string message, bool isAlignLeft)
    {
        if(message.Length > 0)
        {
            ChatData msg = new ChatData();
            msg.IsAlignLeft = isAlignLeft;
            msg.Message = message;
            messages.Add(msg);
            UpdateTimeStamp();
        }
    }

    private void UpdateTimeStamp()
    {
        timeStamp++;
        if (timeStamp <= 0)
            timeStamp = 1;
    }

    public int GetTimeStamp()
    {
        return timeStamp;
    }

    public List<ChatData> GetChatData()
    {
        return messages;
    }
}
