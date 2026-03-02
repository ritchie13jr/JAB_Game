using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    public Image violenceFill;
    public Image lustFill;
    public Image hatredFill;
    public Image mysteryFill;

    public TextMeshProUGUI dayText;
    public TextMeshProUGUI timerText;

    GameManager gm;

    void Start()
    {
        gm = GameManager.Instance;
    }

    void Update()
    {
        UpdateStats();
        UpdateDayTimer();
    }

    void UpdateStats()
    {
        float max = gm.maxForStat;
        float speed = 5f;

        float violenceTarget = gm.stats["VIOLENCE"] / max;
        float lustTarget = gm.stats["LUST"] / max;
        float hatredTarget = gm.stats["HATRED"] / max;
        //float mysteryTarget = gm.stats["PROGRESS"] / max;

        violenceFill.fillAmount = Mathf.Lerp(violenceFill.fillAmount, violenceTarget, Time.deltaTime * speed);
        lustFill.fillAmount = Mathf.Lerp(lustFill.fillAmount, lustTarget, Time.deltaTime * speed);
        hatredFill.fillAmount = Mathf.Lerp(hatredFill.fillAmount, hatredTarget, Time.deltaTime * speed);
        //mysteryFill.fillAmount = Mathf.Lerp(mysteryFill.fillAmount, mysteryTarget, Time.deltaTime * speed);
    }

    void UpdateDayTimer()
    {
        dayText.text = "DAY " + gm.currentDay;

        if (gm.DayTimerProgress)
        {
            float remaining = gm.dayDuration - gm.dayTimer;
            remaining = Mathf.Clamp(remaining, 0, gm.dayDuration);
            timerText.text = FormatTime(remaining);
        }
        else
        {
            timerText.text = gm.cardCounter + "/" + gm.cardsPerDay;
        }
    }

    string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        return minutes.ToString("00") + ":" + seconds.ToString("00");
    }
}