using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : Singleton<GameDataManager>
{
    protected GameDataManager() { }  // private와 동일하지만 상속받은 자식은 쓸수있다.

    float currentHealth = 20f;
    float maxHealth = 20f;

    int timeStamp = 0;  // 값을 바꾸는 모든함수에서 timeStamp를 업데이트해서 기록을 남긴다

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0f)
            currentHealth = 0f;
        UpdateTimeStamp();
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        UpdateTimeStamp();
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
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
}