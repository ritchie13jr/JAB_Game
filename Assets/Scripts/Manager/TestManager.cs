using UnityEngine;
using System.Collections.Generic;

public class TestManager : MonoBehaviour
{
    public static TestManager instance;
    public CardSpawner spawner;

    [Header("Stats")]
    public float StatA;
    public float StatB;
    public float StatC;
    public float StatD;

    public float maxForStat = 100.0f;

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

    void InitializeStats()
    {
        StatA = 25f;
        StatB = 25f;
        StatC = 25f;
        StatD = 25f;
    }

    private void Update()
    {
        StatA -= Time.deltaTime;
        StatB -= Time.deltaTime;
        StatC -= Time.deltaTime;
        StatD -= Time.deltaTime;

        ClampStats();

        if (Input.GetKeyDown(KeyCode.R))
        {
            spawner.SpawnRandomCard();
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
        // Aqui de una lista de cartas que le añadimos o creamos dinamicamente le pasamos la siguiente para que se ponga
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
}
