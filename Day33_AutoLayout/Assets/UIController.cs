using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public InputField inputField;
    public ChatSystem chatSystem;

    int uiTimeStamp = 0;

    // Start is called before the first frame update
    void Start()
    {
        inputField.onEndEdit.AddListener((message) =>   // 이 콜백으로 인풋필드에 엔터를치면 메세지를 넘겨줌
        {
            // Instance?.~~~  =>  Instance가 null이면 뒤를 실행하지않음
            GameDataManager.Instance?.AddMessage(message, UnityEngine.Random.Range(0, 2) == 0 ? true : false);
            //print(message);  != null
            inputField.Select();
            inputField.ActivateInputField();
            inputField.text = string.Empty;
        });
    }

    // Update is called once per frame
    void Update()
    {
        int timeStamp = GameDataManager.Instance.GetTimeStamp();
        if(timeStamp != uiTimeStamp)
        {
            uiTimeStamp = timeStamp;
            List<ChatData> messages = GameDataManager.Instance.GetChatData();
            chatSystem.UpdateChatData(messages);
        }
    }
}
