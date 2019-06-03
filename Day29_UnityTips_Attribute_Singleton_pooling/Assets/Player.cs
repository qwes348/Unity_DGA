using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        print(MobManager.Instance.myGlobalVar == "Whatever");
        print(MobManager.Instance.MobCount() == 100);
    }
}
