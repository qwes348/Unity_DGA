using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealOverTime : MonoBehaviour
{
    public GameObject healFX;
    bool isHealing = false;
    GameObject fx;

    public void Heal()
    {
        if (!isHealing)
        {
            fx = Instantiate(healFX, transform.Find("FXPos"));
            isHealing = true;
            print(isHealing);
            Invoke("RemoveHealFX", 1.9f);  // 1.9초뒤에 RemoveHealFX 함수 실행
        }
    }

    void RemoveHealFX()
    {
        Destroy(fx);
        isHealing = false;
    }
}
