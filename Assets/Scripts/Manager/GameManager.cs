using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public CardSpawner m_Spawner;
    public Fade m_Fade;
    public TextMeshProUGUI m_GameOverText;

    [Header("Win / Lose onditions")]
    public bool LoseOnAnyZero = true;
    public bool LoseOnAnyMax = false;
    private bool m_gameOver;

    [Header("Stats")]
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
        if (Instance == null)
        {
            Instance = this;
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
        stats.Add("VIOLENCE", statInitialValue);
        stats.Add("LUST", statInitialValue);
        stats.Add("HATRED", statInitialValue);
        stats.Add("?", statInitialValue);

        ClampStats();
    }
    public void ModifyStat(List<StatModifier> modifier)
    {
        foreach (var mod in modifier)
        {
            switch (mod.StatType)
            {
                case StatType.StatA:
                    stats["VIOLENCE"] += mod.amount;
                    break;
                case StatType.StatB:
                    stats["LUST"] += mod.amount;
                    break;
                case StatType.StatC:
                    stats["HATRED"] += mod.amount;
                    break;
                case StatType.StatD:
                    stats["?"] += mod.amount;
                    break;
            }
        }

        ClampStats();
    }
    void RestartGame()
    {
        m_gameOver = false;
        m_GameOverText.gameObject.SetActive(false);

        ResetStats();

        m_Fade.FadeOut(() =>
        {
            m_Fade.gameObject.SetActive(false);
            ResetProgressValues();
            startDay = false;
        });
    }

    void ResetStats()
    {
        stats.Clear();
        InitializeStats();
    }

    void ResetProgressValues()
    {
        if (DayTimerProgress) dayTimer = 0.0f;
        else cardCounter = 0;
    }


    private void Update()
    {
        if (m_gameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space)) RestartGame();
            return;
        }

        if (!startDay)
        {
            if (DayTimerProgress)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    StartDayProgress();
                }
                return;
            }
            else { StartDayProgress(); }
        }

        if (DayTimerProgress)
        {
            foreach (var key in stats.Keys.ToList())
            {
                stats[key] -= Time.deltaTime;
            }

            ClampStats();

            DayTimeProgress(Time.deltaTime);
        }

        else
        {
            DayCardProgrss();
        }

        if (CheckGameOverConditions()) GameOver();
    }

    void ClampStats()
    {
        foreach (var key in stats.Keys.ToList())
        {
            stats[key] = Mathf.Clamp(stats[key], 0.0f, maxForStat);
        }
    }

    public void GetNextCard()
    {
        m_Spawner.SpawnRandomCard();
        cardCounter++;

        ClampStats();
    }

    void DayTimeProgress(float deltaTime)
    {
        dayTimer += deltaTime;

        if (dayTimer >= dayDuration)
        {
            GoToNextDayWithFade();
        }
    }
    void DayCardProgrss()
    {
        if (cardCounter > cardsPerDay)
        {
            GoToNextDayWithFade();
            ModifyStat(statDecay);
        }
    }
    void GoToNextDay()
    {
        if (m_Spawner.currentCard != null)
            m_Spawner.currentCard.RemoveCard();

        currentDay++;
        startDay = false;
    }

    void StartDayProgress()
    {
        startDay = true;
        GetNextCard();
    }

    void GoToNextDayWithFade()
    {
        ResetProgressValues();

        if (!DayTimerProgress) m_Spawner.currentCard.RemoveCard();

        m_Fade.FadeIn(() =>
        {
            GoToNextDay();
            m_Fade.FadeOut(() =>
            {
                m_Fade.gameObject.SetActive(false);
            });
        });
    }

    bool CheckGameOverConditions()
    {
        if (LoseOnAnyZero)
        {
            foreach (var value in stats.Values)
            {
                if (value <= 0.0f) return true;
            }
        }

        if (LoseOnAnyMax)
        {
            foreach (var value in stats.Values)
            {
                if (value >= maxForStat) return true;
            }
        }

        return false;
    }

    void GameOver()
    {
        m_gameOver = true;
        m_Fade.FadeIn(() =>
        {
            m_GameOverText.gameObject.SetActive(true);
        });
    }


}
