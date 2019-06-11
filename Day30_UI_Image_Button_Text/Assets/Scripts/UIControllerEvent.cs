using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControllerEvent : MonoBehaviour
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
        damageButton.onClick.AddListener(() => GameDataManagerEvent.Instance.TakeDamage(10));
        healButton.onClick.AddListener(() => GameDataManagerEvent.Instance.Heal(20));

        p2DamageButton.onClick.AddListener(() => GameDataManagerEvent.Instance.P2TakeDamage(10));
        p2HealButton.onClick.AddListener(() => GameDataManagerEvent.Instance.P2Heal(20));

        //StartCoroutine(TimerStart());
        StartCoroutine(StartTimer());
    }

    // Update is called once per frame
    void OnDataUpdate()  // MVC == 모델 뷰 컨트롤러
    {
        //int timeStamp = GameDataManagerEvent.Instance.GetTimeStamp();
        //if(timeStamp != uiTimeStamp)  // timeStamp가 바뀌었다면 모든UI를 업데이트한다 Polling 이라고함
        {
            //uiTimeStamp = timeStamp;
            float currentHealth = GameDataManagerEvent.Instance.GetCurrentHealth();
            float p2CurrentHealth = GameDataManagerEvent.Instance.P2GetCurrentHealth();
            float maxHealth = GameDataManagerEvent.Instance.GetMaxHealth();
            leftSide.UpdateHealthBart(currentHealth, maxHealth);
            rightSide.UpdateHealthBart(p2CurrentHealth, maxHealth);

            int timeCount = GameDataManagerEvent.Instance.GetTimeCount();  // Lecture
            timerText.text = timeCount.ToString();  // Lecture

        }
    }

    private void OnEnable()
    {
        GameDataManagerEvent.Instance.OnDataChanged += OnDataUpdate;
    }

    private void OnDisable()
    {
        if (GameDataManager.Instance != null)
            GameDataManagerEvent.Instance.OnDataChanged -= OnDataUpdate;
    }

    IEnumerator TimerStart() // HW
    {
        yield return new WaitForSeconds(1f);

        GameDataManagerEvent.Instance.SetTimeCount((GameDataManagerEvent.Instance.GetTimeCount() - 1));
        timerText.text = GameDataManagerEvent.Instance.GetTimeCount().ToString();

        StartCoroutine(TimerStart());
    }

    IEnumerator StartTimer()  // Lecture
    {
        int timeCount = GameDataManagerEvent.Instance.GetTimeCount();
        while (timeCount >= 0)
        {
            GameDataManagerEvent.Instance.SetTimeCount(--timeCount);
            yield return new WaitForSeconds(1f);
        }
    }
}
