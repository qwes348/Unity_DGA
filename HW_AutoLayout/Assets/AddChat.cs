using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddChat : MonoBehaviour
{
    public GameObject helloChat;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            int rnd = Random.Range(0, 2);
            
            if(rnd == 0)
            {
                Instantiate(helloChat, transform.position, Quaternion.identity, transform);
            }
            if(rnd == 1)
            {
                var right = Instantiate(helloChat, transform.position, Quaternion.identity, transform);
                right.GetComponent<HorizontalLayoutGroup>().childAlignment = TextAnchor.MiddleRight;
                right.GetComponent<HorizontalLayoutGroup>().padding.right = 0;
                right.GetComponent<HorizontalLayoutGroup>().padding.left = 200;
                var bubble = right.transform.GetChild(0);
                bubble.Rotate(new Vector3(0f, 180f, 0f));
                bubble.GetComponent<Image>().color = new Color32(63, 141, 233, 255);

                var text = bubble.transform.GetChild(0);
                text.Rotate(new Vector3(0f, 180f, 0f));
                text.GetComponent<Text>().alignment = TextAnchor.MiddleRight;
                text.GetComponent<Text>().color = new Color32(255, 255, 255, 255);


            }
        }
    }
}
