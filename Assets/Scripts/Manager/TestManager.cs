using UnityEngine;
using System.Collections.Generic;

public class TestManager : MonoBehaviour
{
    public static TestManager instance;
    public CardSpawner spawner;

    public int currentDay = 0; //!
    public float dayDuration = 24.0f;
    public float dayTimer; //!

    [Header("Stats")]
    public float StatA; //!
    public float StatB; //!
    public float StatC; //!
    public float StatD; //!

    public float maxForStat = 100.0f;
    public float statInitialValue = 25.0f;

    private bool startDay = false;

    public bool DayTimerProgress = true;

    public int cardsPerDay = 3;
    public int cardCounter = -1; //!

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeStats();
        }
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        GoToNextDay();
    }

    void InitializeStats()
    {
        StatA = statInitialValue;
        StatB = statInitialValue;
        StatC = statInitialValue;
        StatD = statInitialValue;
    }

    private void Update()
    {


        if (!startDay)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartDayProgress();
            }
            return;
        }

        if (DayTimerProgress)
        {
            StatA -= Time.deltaTime;
            StatB -= Time.deltaTime;
            StatC -= Time.deltaTime;
            StatD -= Time.deltaTime;

            ClampStats();

            DayTimeProgress(Time.deltaTime);
        }

        else
        {
            DayCardProgrss();
        }

       

       
    }

    void ClampStats()
    {
        StatA = Mathf.Clamp(StatA, 0.0f, maxForStat);
        StatB = Mathf.Clamp(StatB, 0.0f, maxForStat);
        StatC = Mathf.Clamp(StatC, 0.0f, maxForStat);
        StatD = Mathf.Clamp(StatD, 0.0f, maxForStat);
    }

    public void GetNextCard()
    {
        spawner.SpawnRandomCard();
        ClampStats();
        cardCounter++;
    }

    public void ModifyStat(List<StatModifier> modifier)
    {
        foreach (var mod in modifier)
        {
            switch (mod.StatType)
            {
                case StatType.StatA:
                    StatA += mod.amount;
                    break;
                case StatType.StatB:
                    StatB += mod.amount;
                    break;
                case StatType.StatC:
                    StatC += mod.amount;
                    break;
                case StatType.StatD:
                    StatD += mod.amount;
                    break;
            }
        }

        ClampStats();
    }

    public void DayTimeProgress(float deltaTime)
    {
        dayTimer += deltaTime;

        if (dayTimer >= dayDuration)
        {
            GoToNextDay();
        }
    }
    public void DayCardProgrss()
    {
        if (cardCounter > cardsPerDay)
        {
            GoToNextDay();
        }
    }
    public void GoToNextDay()
    {
        if (spawner.currentCard != null)
        spawner.currentCard.RemoveCard();

        currentDay++;
        startDay = false;

        if (DayTimerProgress) dayTimer = 0;
        else cardCounter = -1;
    }

    public void StartDayProgress()
    {
        startDay = true;
        GetNextCard();
    }
}
