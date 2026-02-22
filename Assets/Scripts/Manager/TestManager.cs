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

    private bool startDay = false;

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
        StatA = 25f;
        StatB = 25f;
        StatC = 25f;
        StatD = 25f;
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

        StatA -= Time.deltaTime;
        StatB -= Time.deltaTime;
        StatC -= Time.deltaTime;
        StatD -= Time.deltaTime;

        ClampStats();

        DayProgress(Time.deltaTime);
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
    }

    public void ModifyStat(List<StatModifier> modifier)
    {
        foreach (var mod in modifier)
        {
            switch (mod.StatType)
            {
                case StatType.StatA:
                    StatA += mod.amount;
                    return;
                case StatType.StatB:
                    StatB += mod.amount;
                    return;
                case StatType.StatC:
                    StatC += mod.amount;
                    return;
                case StatType.StatD:
                    StatD += mod.amount;
                    return;
            }
        }

        ClampStats();
    }

    public void DayProgress(float deltaTime)
    {
        dayTimer += deltaTime;

        if (dayTimer >= dayDuration)
        {
            GoToNextDay();
        }
    }
    public void GoToNextDay()
    {
        if (spawner.currentCard != null)
        spawner.currentCard.RemoveCard();

        dayTimer = 0;
        currentDay++;

        startDay = false;
    }

    public void StartDayProgress()
    {
        startDay = true;
        GetNextCard();
    }
}
