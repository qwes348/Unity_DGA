using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScriptableObject : MonoBehaviour
{
    [SerializeField]
    ItemData healthPotion;

    void Start()
    {
        healthPotion = Resources.Load<ItemData>("ScriptableObject/HealthPotion");      // 파일 경로를 적을때 /가 없으면 현재Dir, /가 있으면 루트Dir(최상위)
    }
}
