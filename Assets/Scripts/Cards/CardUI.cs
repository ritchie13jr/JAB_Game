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
        GameManager.Instance.ModifyStat(cardData.publishEffects);
        GameManager.Instance.GetNextCard();
        RemoveCard();
    }

    public void OnNoButton()
    {
        GameManager.Instance.GetNextCard();
        RemoveCard();
    }

    public void RemoveCard()
    {
        Destroy(gameObject);
    }
}
