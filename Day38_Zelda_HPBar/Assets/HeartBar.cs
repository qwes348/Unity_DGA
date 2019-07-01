using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heart
{
    public Image foregroundImage;
    public Image[] glowImages;
    public RectTransform glow;    

    public Heart(int maxHealth)
    {
        glowImages = new Image[maxHealth];
    }

    public void TurnOffAllGlows()
    {
        for (int i = 0; i < glowImages.Length; i++)
        {
            TurnOffGlowAt(i);
        }
    }

    public void TurnOffGlowAt(int i)
    {
        glowImages[i].enabled = false;
        glowImages[i].fillAmount = 0f;
    }

    public void TurnOnGlowAt(int i)
    {        
        glowImages[i].enabled = true;
        glowImages[i].fillAmount = 1f;
    }
}

public class HeartBar : MonoBehaviour
{
    public GameObject heartPrefab;
    public int totalHealth;
    public int CurrentHealth { get; private set; }
    const int healthPerHeart = 4;

    List<Heart> list;
    private Coroutine coAnimDecreaseHP;
    private Coroutine coAnimIncreaseHP;
    private int previousAmount;
    private bool isAfterDecHP;
    private bool isAfterIncHP;

    private void Awake()
    {
        list = new List<Heart>();
        CurrentHealth = totalHealth;
        int remainHealth = totalHealth;
        
        while(remainHealth > 0)
        {
            if (remainHealth < healthPerHeart)      // 하트가 딱떨어지지않을때 마지막거는 덜채움
                list.Add(new Heart(remainHealth));
            else
                list.Add(new Heart(healthPerHeart));
            remainHealth -= healthPerHeart;
        }
        foreach(var h in list)
        {
            var clone = Instantiate(heartPrefab, transform);
            h.foregroundImage = clone.transform.Find("Background/Foreground").GetComponent<Image>();
            h.glow = (RectTransform)clone.transform.Find("Glow");
            for (int i = 0; i < h.glowImages.Length; i++)
            {
                h.glowImages[i] = h.glow.GetChild(i).GetComponent<Image>();
            }
            h.TurnOffAllGlows();
        }
        FillAllHearts();
    }

    private void FillAllHearts()
    {
        int i = 0;
        foreach (var h in list)
        {
            int heartIndex = i / healthPerHeart;
            int currentHeartIndex = CurrentHealth / healthPerHeart;    // currentHeartIndex 기준으로 이앞은 FullHeart 이 뒤는 EmptyHeart
            if(heartIndex < currentHeartIndex)
            {
                h.foregroundImage.fillAmount = 1f;
            }
            else if(heartIndex == currentHeartIndex)
            {
                h.foregroundImage.fillAmount = (float)(CurrentHealth % healthPerHeart) / healthPerHeart;
            }
            else if(heartIndex > currentHeartIndex)
            {
                h.foregroundImage.fillAmount = 0f;
            }

            i += healthPerHeart;
        }
    }

    public void DecreaseHP(int amount)
    {
        StopAnim(previousAmount);
        previousAmount = amount;
        coAnimDecreaseHP = StartCoroutine(AnimDecreaseHP(amount));
    }

    public void IncreaseHP(int amount)
    {
        StopAnim(previousAmount);
        previousAmount = amount;
        coAnimIncreaseHP = StartCoroutine(AnimIncreaseHP(amount));
    }

    private void StopAnim(int previousAmount)
    {
        if (coAnimDecreaseHP != null)
        {
            if (!isAfterDecHP)
            {
                DecHP(previousAmount);
            }
            foreach (var h in list)
            {
                h.TurnOffAllGlows();
            }
            FillAllHearts();
            StopCoroutine(coAnimDecreaseHP);
            coAnimDecreaseHP = null;
        }

        if (coAnimIncreaseHP != null)
        {
            if (!isAfterIncHP)
            {
                IncHP(previousAmount);
            }
            foreach (var h in list)
            {
                h.TurnOffAllGlows();
            }
            FillAllHearts();
            StopCoroutine(coAnimIncreaseHP);
            coAnimIncreaseHP = null;
        }
    }
    IEnumerator AnimDecreaseHP(int amount)
    {
        if (amount <= 0 || CurrentHealth == 0)
            yield break;
        
        int last = CurrentHealth - 1;
        int first = CurrentHealth - amount > 0 ? CurrentHealth - amount : 0;

        // glow
        isAfterDecHP = false;
        FillGlow(first, last);
        yield return StartCoroutine(AnimGlow());
        DecHP(amount);
        isAfterDecHP = true;

        FillAllHearts();

        for (int i = last; i >= first; i--)
        {
            Image glow = GetGlowImageAtHealthPoint(i);
            glow.fillClockwise = false;
            float begin = 0;
            float f = 1f;

            while(f > begin)
            {
                glow.fillAmount = f;
                f -= 5f * (last - first + 1) * Time.deltaTime;
                yield return null;
            }
            glow.fillAmount = begin;
        }
    }

    IEnumerator AnimIncreaseHP(int amount)
    {
        if (amount <= 0 || CurrentHealth == totalHealth)
            yield break;

        int last = CurrentHealth + amount > totalHealth ? totalHealth-1 : CurrentHealth + amount-1;
        int first = CurrentHealth;

        isAfterIncHP = false;
        FillGlow(first, last);
        yield return StartCoroutine(AnimGlow());
        IncHP(amount);
        isAfterIncHP = true;

        FillAllHearts();

        for (int i = first; i <= last; i++)
        {
            Image glow = GetGlowImageAtHealthPoint(i);
            glow.fillClockwise = true;
            float begin = 0;
            float f = 1f;

            while(f > begin)
            {
                glow.fillAmount = f;
                f -= 5f * (last - first + 1) * Time.deltaTime;
                yield return null;
            }
            glow.fillAmount = begin;
        }
    }

    private Image GetGlowImageAtHealthPoint(int i)
    {        
        return GetHeartAtHealthPoint(i).glowImages[i % healthPerHeart];
    }

    private void DecHP(int amount)
    {
        CurrentHealth -= amount;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, totalHealth);
    }

    private void IncHP(int amount)
    {
        CurrentHealth += amount;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, totalHealth);
    }

    IEnumerator AnimGlow()
    {
        foreach (var h in list)
        {
            h.glow.localScale = Vector3.one * 1.9f;
        }
        yield return new WaitForSeconds(0.3f);
        foreach (var h in list)
        {
            h.glow.localScale = Vector3.one * 1.5f;
        }
    }

    private void FillGlow(int from, int to)
    {        
        for (int i = from; i <= to; i++)
        {
            Heart h = GetHeartAtHealthPoint(i);
            h.TurnOnGlowAt(i % healthPerHeart);
        }
    }

    private Heart GetHeartAtHealthPoint(int i)
    {
        return list[i / healthPerHeart];
    }

}
