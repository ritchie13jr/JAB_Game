using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CardUI : MonoBehaviour
{
    public TextMeshProUGUI titleTxt;
    public TextMeshProUGUI descriptionTxt;
    public Button publishButton;
    public Button noButton;
    public Image backgroundImg;

    private CardData cardData;

    [Header("Sound Effects")]
    public AudioClip publishSFX;
    public AudioClip noSFX;

    public void Setup(CardData card)
    {
        cardData = card;
        titleTxt.text = (card.title != "")? card.title : card.name;
        descriptionTxt.text = card.description;
        backgroundImg.sprite = card.background;


        publishButton.onClick.AddListener(OnPublishedButton);
        noButton.onClick.AddListener(OnNoButton);
    }

    public void OnPublishedButton()
    {
        SoundManager.Instance.PlaySFX(publishSFX);

        GameManager.Instance.ModifyStat(cardData.publishEffects);
        GameManager.Instance.m_Spawner.RemoveCard(cardData);
        GameManager.Instance.GetNextCard();
        RemoveCard();
    }

    public void OnNoButton()
    {
        SoundManager.Instance.PlaySFX(noSFX);

        GameManager.Instance.GetNextCard();
        RemoveCard();
    }

    public void RemoveCard()
    {
        Destroy(gameObject);
    }
}
