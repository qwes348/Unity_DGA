using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Health leftSide;
    public Health rightSide;
    public Button damageButton;
    public Button healButton;
    public Text timerText;

    public Button p2DamageButton;
    public Button p2HealButton;

    int uiTimeStamp = 0;

    // Start is called before the first frame update
    void Start()
    {
        damageButton.onClick.AddListener(() => GameDataManager.Instance.TakeDamage(10));
        healButton.onClick.AddListener(() => GameDataManager.Instance.Heal(20));

        p2DamageButton.onClick.AddListener(() => GameDataManager.Instance.P2TakeDamage(10));
        p2HealButton.onClick.AddListener(() => GameDataManager.Instance.P2Heal(20));

        StartCoroutine(TimerStart());
    }

    // Update is called once per frame
    void Update()  // MVC == 모델 뷰 컨트롤러
    {
        int timeStamp = GameDataManager.Instance.GetTimeStamp();
        if(timeStamp != uiTimeStamp)  // timeStamp가 바뀌었다면 모든UI를 업데이트한다 Polling 이라고함
        {
            uiTimeStamp = timeStamp;
            float currentHealth = GameDataManager.Instance.GetCurrentHealth();
            float p2CurrentHealth = GameDataManager.Instance.P2GetCurrentHealth();
            float maxHealth = GameDataManager.Instance.GetMaxHealth();
            leftSide.UpdateHealthBart(currentHealth, maxHealth);
            rightSide.UpdateHealthBart(p2CurrentHealth, maxHealth);

        }
    }

    IEnumerator TimerStart()
    {
        yield return new WaitForSeconds(1f);

        GameDataManager.Instance.SetTimeCount((GameDataManager.Instance.GetTimeCount() - 1));
        timerText.text = GameDataManager.Instance.GetTimeCount().ToString();

        StartCoroutine(TimerStart());
    }
}
