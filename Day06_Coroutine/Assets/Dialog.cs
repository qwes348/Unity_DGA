using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog : MonoBehaviour
{
    bool showDialog = false;
    string answer = "";
    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return StartCoroutine("ShowDialog");
        yield return StartCoroutine(answer);  // answer에는 ActionA or ActionB가 들어가기때문에 결국 스트링으로 코루틴호출하는것이 됨
    }

    IEnumerator ShowDialog()
    {
        showDialog = true;
        do
        {
            yield return null;
        } while (answer == "");  // answer가 empty가 아니면 클릭한것이므로 화면에서 버튼을 지운다
        showDialog = false;
    }

    IEnumerator ActionA()
    {
        print("ActionA");
        yield return new WaitForSeconds(1f);
    }
    IEnumerator ActionB()
    {
        print("ActionB");
        yield return new WaitForSeconds(1f);
    }

    private void OnGUI()  // 옛날스타일 UI만들기 지금은 잘 안씀
    {
        if(showDialog)
        {
            if(GUI.Button(new Rect(10f, 10f, 100f, 20f), "A"))
            {
                answer = "ActionA";
            }
            else if (GUI.Button(new Rect(10f, 50f, 100f, 20f), "B"))
            {
                answer = "ActionB";
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
