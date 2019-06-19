using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class HandloingJson : MonoBehaviour
{
    string pathJson;


    // Start is called before the first frame update
    void Start()
    {
        pathJson = Application.persistentDataPath + "/gameScore.json";
        print(pathJson);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            SaveJsonFile();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadJsonFile();
        }
    }

    private void LoadJsonFile()
    {
        if(File.Exists(pathJson))
        {
            string dataAsJson = File.ReadAllText(pathJson);
            GameScore newGS = JsonUtility.FromJson<GameScore>(dataAsJson);
            print(newGS);  // override Tostring
        }
        else
        {
            print("no File !!");
        }
    }

    private void SaveJsonFile()
    {
        GameScore gs = new GameScore();
        gs.level = 10;
        gs.timeElapsed = 300;
        gs.playerName = "DGA";
        gs.dontCare = "dontcare";

        Item item = new Item();
        item.itemid = 1000;
        item.iconImage = "image1.png";
        item.price = 10000;

        gs.items = new List<Item>();
        gs.items.Add(item);

        Item item1 = new Item();
        item1.itemid = 2000;
        item1.iconImage = "image2.png";
        item1.price = 7777;

        gs.items.Add(item1);

        string dataAsJson = JsonUtility.ToJson(gs, true);  // 두번째 Parameter는 PrettyPrint 말그대로 줄바꿈을 해줘서 보기좋게 만들어준다
        print(dataAsJson);
        File.WriteAllText(pathJson, dataAsJson);        // Sync == Blocked 저장하는동안 멈춤        
    }
}

[Serializable]
public class GameScore
{
    public int level;
    public float timeElapsed;
    public string playerName;

    [NonSerialized]
    public string dontCare;

    public List<Item> items;

    public override string ToString()
    {
        return level + ", " + timeElapsed + ", " + playerName + ", " + items.Count;
    }
}

[Serializable]
public class Item
{
    public int itemid;
    public string iconImage;
    public int price;
}
