using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public List<Image> Hearts;
    public bool isDie = false;
    public bool isFullRecovery = true;

    public Image currentHeart;
    public Image currentGlow;
    //public List<Image> glowHearts;

    //public void UpdateHearts(int damageAmount, int healAmount)
    //{
    //    if (!isDie)
    //    {
    //        StartCoroutine(SlowlyDamage(damageAmount));
    //        isDie = true;
    //    }
    //    if (!isFullRecovery)
    //    {
    //        StartCoroutine(SlowlyHeal(healAmount));
    //    }
    //}


    public IEnumerator SlowlyDamage(int damageAmount)
    {
        int length = Hearts.Count - 1;
        int i = length;
        currentHeart = Hearts[i];
        currentGlow = Hearts[i].transform.GetChild(1).GetComponent<Image>();
        isDie = GameDataManager.Instance.isDie;

        if (!isDie)  // 비어있는 하트는 통과 만약 모두 빈하트라면 DIE
        {
            while (i > 0 && Hearts[i].GetComponent<Image>().fillAmount == 0f)
            {
                i--;
            }
        }

        while(damageAmount > 0/*damageAmount < 4*/)  // 1~3까지 데미지일때
        {
            if (i < 0)
            {
                break;
            }
            currentHeart = Hearts[i];
            currentGlow = Hearts[i].transform.GetChild(1).GetComponent<Image>();

            currentGlow.fillAmount = damageAmount * 0.25f;  // 데미지 양만큼의 GlowHeart 소환
            if (currentGlow.fillAmount > currentHeart.fillAmount) 
            {
                currentGlow.fillAmount = currentHeart.fillAmount;
            }

            currentGlow.fillOrigin = (int)Image.Origin360.Top;
            currentGlow.fillClockwise = false;

            if (damageAmount == 1 && currentHeart.fillAmount == 1f)
            {
                currentGlow.fillOrigin = (int)Image.Origin360.Right;
                currentGlow.fillClockwise = false;
            }
            if (damageAmount == 1 && currentHeart.fillAmount == 0.75f)
            {
                currentGlow.fillOrigin = (int)Image.Origin360.Bottom;
                currentGlow.fillClockwise = false;
            }
            if (damageAmount == 1 && currentHeart.fillAmount == 0.5f)
            {
                currentGlow.fillOrigin = (int)Image.Origin360.Left;
                currentGlow.fillClockwise = false;
            }

            currentGlow.gameObject.SetActive(true);
            currentHeart.fillAmount -= currentGlow.fillAmount;

            damageAmount -= Mathf.FloorToInt(currentGlow.fillAmount * 4);   // 남은데미지 계산
            i--;
        }
        for(i = length; i >= 0; i--)     // 데미지 애니메이션 구현부분
        {
            currentGlow = Hearts[i].transform.GetChild(1).GetComponent<Image>();
            if(currentGlow.gameObject.activeSelf)
            {                    
                while (currentGlow.fillAmount > 0)
                {
                    yield return null;
                    currentGlow.fillAmount -= 0.05f;
                }
            }
        }
        if(GameDataManager.Instance.GetCurrentHealth() == 0f)
        {
            GameDataManager.Instance.isDie = true;
        }
        GameDataManager.Instance.isFullRecovery = false;
    }

    public IEnumerator SlowlyHeal(int healAmount)
    {        
        int length = Hearts.Count - 1;
        print("length: " + length);
        int i = 0;
        print(i);
        currentHeart = Hearts[i];
        currentGlow = Hearts[i].transform.GetChild(1).GetComponent<Image>();
        print("pass");
        isFullRecovery = GameDataManager.Instance.isFullRecovery;

        if (!isFullRecovery)  
        {
            print("pass2");
            while (i < length && Hearts[i].fillAmount == 1f)
            {
                i++;
                print(i);
            }
        }

        while (healAmount > 0) 
        {
            if (i > length)
            {
                break;
            }

            currentHeart = Hearts[i];
            currentGlow = Hearts[i].transform.GetChild(1).GetComponent<Image>();
            print("pass3");
            print("while i: " + i);

            currentGlow.fillAmount = healAmount * 0.25f;
            if (currentGlow.fillAmount > currentHeart.fillAmount)
            {
                currentHeart.fillAmount = currentGlow.fillAmount;
            }

            currentGlow.fillOrigin = (int)Image.Origin360.Top;
            currentGlow.fillClockwise = true;

            currentGlow.gameObject.SetActive(true);
            currentHeart.fillAmount += currentGlow.fillAmount;

            healAmount -= Mathf.FloorToInt(currentGlow.fillAmount * 4);  
            i++;
        }
        for (i = 0; i <= length; i++) 
        {
            currentGlow = Hearts[i].transform.GetChild(1).GetComponent<Image>();
            if (currentGlow.gameObject.activeSelf)
            {
                while (currentGlow.fillAmount > 0)
                {
                    yield return null;
                    currentGlow.fillAmount -= 0.05f;
                }
            }
        }
        if (GameDataManager.Instance.GetCurrentHealth() == GameDataManager.Instance.GetMaxHealth())
        {
            //GameDataManager.Instance.isFullRecovery = true;
        }
        GameDataManager.Instance.isDie = false;
    }

}
