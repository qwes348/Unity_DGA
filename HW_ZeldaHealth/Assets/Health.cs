using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public List<Image> Hearts;
    public List<Image> glowHearts;

    public void UpdateHearts(int damageAmount)
    {        
        for (int i = Hearts.Count-1; i >= 0;)
        {
            if (damageAmount == 0)
                break;
            if (Hearts[i].fillAmount == 0)
            {
                i--;
                continue;
            }
            StartCoroutine(SlowlyDamage(i));
            damageAmount--;
        }
    }

    IEnumerator SlowlyDamage(int i)
    {
        print("co ok");
        float finalHealth = Hearts[i].fillAmount - 0.25f;
        while(finalHealth < Hearts[i].fillAmount)
        {
            Hearts[i].fillAmount -= 0.01f;
            yield return new WaitForSeconds(0.01f);
        }
    }

}
