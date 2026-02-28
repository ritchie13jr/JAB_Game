using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class TestManager : MonoBehaviour
{
    public static TestManager instance;
    public CardSpawner spawner;
    public Fade UI;

    [Header("Win / Lose onditions")]
    public bool LoseOnAnyZero = true;

    [Header("Stats")]
    //public float StatA; //!
    //public float StatB; //!
    //public float StatC; //!
    //public float StatD; //!
    private Dictionary<string, float> stats = new Dictionary<string, float>();

    public float maxForStat = 100.0f;
    public float statInitialValue = 25.0f;

    public List<StatModifier> statDecay;

    [Header("DayProgress")]
    public int currentDay = 0; //!
    private bool startDay = false;

    public bool DayTimerProgress = true;

    [Header("If DayTimerProgress is true")]
    public float dayDuration = 24.0f;
    public float dayTimer; //!


    [Header("If DayTimerProgress is false")]
    public int cardsPerDay = 3;
    public int cardCounter = 0; //!


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        InitializeStats();
        GoToNextDay();
    }

    void InitializeStats()
    {
        stats.Add("StatA", statInitialValue);
        stats.Add("StatB", statInitialValue);
        stats.Add("StatC", statInitialValue);
        stats.Add("StatD", statInitialValue);
        //StatA = statInitialValue;
        //StatB = statInitialValue;
        //StatC = statInitialValue;
        //StatD = statInitialValue;
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
            foreach (var key in stats.Keys)
            {
                stats[key] -= Time.deltaTime;
            }
            //StatA -= Time.deltaTime;
            //StatB -= Time.deltaTime;
            //StatC -= Time.deltaTime;
            //StatD -= Time.deltaTime;

            ClampStats();

            DayTimeProgress(Time.deltaTime);
        }

        else
        {
            DayCardProgrss();
        }


        if (CheckGameOverConditions()) Debug.Log("GAME OVER");

    }

    void ClampStats()
    {
        foreach (var key in stats.Keys.ToList())
        {
            stats[key] = Mathf.Clamp(stats[key], 0.0f, maxForStat);
        }
        //StatA = Mathf.Clamp(StatA, 0.0f, maxForStat);
        //StatB = Mathf.Clamp(StatB, 0.0f, maxForStat);
        //StatC = Mathf.Clamp(StatC, 0.0f, maxForStat);
        //StatD = Mathf.Clamp(StatD, 0.0f, maxForStat);
    }

    public void GetNextCard()
    {
        spawner.SpawnRandomCard();
        cardCounter++;

        ClampStats();
    }

    public void ModifyStat(List<StatModifier> modifier)
    {
        foreach (var mod in modifier)
        {
            switch (mod.StatType)
            {
                case StatType.StatA:
                    stats["StatA"] += mod.amount;
                    //StatA += mod.amount;
                    break;
                case StatType.StatB:
                    stats["StatB"] += mod.amount;
                    //StatB += mod.amount;
                    break;
                case StatType.StatC:
                    stats["StatC"] += mod.amount;
                    //StatC += mod.amount;
                    break;
                case StatType.StatD:
                    stats["StatD"] += mod.amount;
                    //StatD += mod.amount;
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
            GoToNextDayWithFade();
        }
    }
    public void DayCardProgrss()
    {
        if (cardCounter > cardsPerDay)
        {
            GoToNextDayWithFade();
            ModifyStat(statDecay);
        }
    }
    public void GoToNextDay()
    {
        if (spawner.currentCard != null)
            spawner.currentCard.RemoveCard();

        currentDay++;
        startDay = false;

        //ResetProgressValues(); //?
    }

    public void StartDayProgress()
    {
        startDay = true;
        GetNextCard();
    }

    public void GoToNextDayWithFade()
    {
        //Debug.Log("GoToNextDayWithFade called");
        ResetProgressValues();

        if (!DayTimerProgress) spawner.currentCard.RemoveCard();

        UI.FadeIn(() =>
        {
            GoToNextDay();
            UI.FadeOut(() =>
            {
                UI.gameObject.SetActive(false);
            });
        });
    }

    public void ResetProgressValues()
    {
        if (DayTimerProgress) dayTimer = 0.0f;
        else cardCounter = 0;
    }

    public bool CheckGameOverConditions()
    {
        if (LoseOnAnyZero)
        {
            foreach (var value in stats.Values)
            {
                if (value <= 0.0f) return true;
            }
        }

        //if (StatA == 0) return true;
        //if (StatB == 0) return true;
        //if (StatC == 0) return true;
        //if (StatD == 0) return true;

        return false;
    }
}
