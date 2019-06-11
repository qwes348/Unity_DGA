using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManagerEvent : Singleton<GameDataManagerEvent>
{
    protected GameDataManagerEvent() { }  // private와 동일하지만 상속받은 자식은 쓸수있다.

    float currentHealth = 100f;
    float maxHealth = 100f;
    int timeCount = 99;

    float p2CurrentHealth = 100f;

    int timeStamp = 0;  // 값을 바꾸는 모든함수에서 timeStamp를 업데이트해서 기록을 남긴다

    public event Action OnDataChanged;
    int actionTimeStamp = 0;

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0f)
            currentHealth = 0f;
        UpdateTimeStamp();
    }

    public void P2TakeDamage(float amount)
    {
        p2CurrentHealth -= amount;
        if (p2CurrentHealth <= 0f)
            p2CurrentHealth = 0f;
        UpdateTimeStamp();
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        UpdateTimeStamp();
    }

    public void P2Heal(float amount)
    {
        p2CurrentHealth += amount;
        p2CurrentHealth = Mathf.Clamp(p2CurrentHealth, 0f, maxHealth);
        UpdateTimeStamp();
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }
    public float P2GetCurrentHealth()
    {
        return p2CurrentHealth;
    }
    public float GetMaxHealth()
    {
        return maxHealth;
    }
    
    public int GetTimeCount()
    {
        return timeCount;
    }

    public void SetTimeCount(int t)
    {
        timeCount = t;
        UpdateTimeStamp();
    }

    void UpdateTimeStamp()  // 값을바꾸는 작업을하면 호출하는 함수
    {
        timeStamp++;
        if (timeStamp <= 0)
            timeStamp = 1;
    }
    public int GetTimeStamp()
    {
        return timeStamp;
    }

    private void Update()
    {
        if(actionTimeStamp != timeStamp)
        {
            actionTimeStamp = timeStamp;
            if(OnDataChanged != null)
            {
                // OnDataChanged(); or
                OnDataChanged.Invoke();
            }
        }
    }

}
