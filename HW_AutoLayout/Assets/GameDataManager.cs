using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatData
{
    public string Message { get; set; }
    public bool IsAlignLeft { get; set; }
}

public class PostData
{
    public Image ProfilePic { get; set; }
    public string FriendName { get; set; }
    public string Location { get; set; }
    public Image Photo { get; set; }
    public string Likes { get; set; }
    public string Description { get; set; }

}
public class GameDataManager : Singleton<GameDataManager>
{
    protected GameDataManager() { }

    List<ChatData> messages = new List<ChatData>();
    List<PostData> posts = new List<PostData>();
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

    public void AddPost(Image profile, string friendName, string location, Image photo, string likes, string description)
    {
        PostData post = new PostData();
        post.ProfilePic = profile;
        post.FriendName = friendName;
        post.Location = location;
        post.Photo = photo;
        post.Likes = likes;
        post.Description = description;

        posts.Add(post);
        UpdateTimeStamp();
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

    public List<PostData> GetPostData()
    {
        return posts;
    }
}
