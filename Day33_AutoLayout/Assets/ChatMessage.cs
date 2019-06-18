using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatMessage : MonoBehaviour
{
    public Color alignLeftColor;
    public Color alignRightColor;

    public bool isAlignLeft;
    public bool IsAlignLeft
    {
        get
        {
            return IsAlignLeft;
        }
        set
        {
            isAlignLeft = value;
            Transform bubble = transform.GetChild(0);
            if(isAlignLeft)
            {
                bubble.localRotation = Quaternion.AngleAxis(0, Vector3.up);
                bubble.GetComponent<Image>().color = alignLeftColor;
                bubble.GetChild(0).localRotation = Quaternion.AngleAxis(0, Vector3.up);
                var hlg = GetComponent<HorizontalLayoutGroup>();
                hlg.childAlignment = TextAnchor.MiddleLeft;
                hlg.padding = new RectOffset(0, 200, 0, 0);
                
            }
            else
            {
                bubble.localRotation = Quaternion.AngleAxis(180, Vector3.up);
                bubble.GetComponent<Image>().color = alignRightColor;
                bubble.GetChild(0).localRotation = Quaternion.AngleAxis(180, Vector3.up);
                var hlg = GetComponent<HorizontalLayoutGroup>();
                hlg.childAlignment = TextAnchor.MiddleRight;
                hlg.padding = new RectOffset(200, 0, 0, 0);
                GetComponentInChildren<Text>().color = Color.white;
            }
        }
    }
    
    public void SetMessage(string message, bool isAlignLeft)
    {
        IsAlignLeft = isAlignLeft;
        GetComponentInChildren<Text>().text = message;
    }
}
