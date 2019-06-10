using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Health Heart;
    public Health glowHeart;

    public Button damageButton;
    public Button healButton;

    public int damageAmount;
    public int healAmount;

    int uiTimeStamp = 0;

    // Start is called before the first frame update
    void Start()
    {
        damageButton.onClick.AddListener(() => GameDataManager.Instance.TakeDamage(damageAmount));
        healButton.onClick.AddListener(() => GameDataManager.Instance.Heal(healAmount));
    }

    // Update is called once per frame
    void Update()  // MVC == 모델 뷰 컨트롤러
    {
        int timeStamp = GameDataManager.Instance.GetTimeStamp();
        if (timeStamp != uiTimeStamp)  // timeStamp가 바뀌었다면 모든UI를 업데이트한다 Polling 이라고함
        {
            uiTimeStamp = timeStamp;
            float currentHealth = GameDataManager.Instance.GetCurrentHealth();
            float maxHealth = GameDataManager.Instance.GetMaxHealth();
            Heart.UpdateHearts(damageAmount);

        }
    }
}
