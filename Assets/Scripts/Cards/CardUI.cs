using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CardUI : MonoBehaviour
{
    public TextMeshProUGUI titleTxt;
    public TextMeshProUGUI descriptionTxt;
    public Button publishButton;
    public Button noButton;

    private CardData cardData;

    public void Setup(CardData card)
    {
        cardData = card;
        titleTxt.text = card.title;
        descriptionTxt.text = card.description;

        publishButton.onClick.AddListener(OnPublishedButton);
        noButton.onClick.AddListener(OnNoButton);
    }

    public void OnPublishedButton()
    {
        TestManager.instance.ModifyStat(cardData.publishEffects);
        TestManager.instance.GetNextCard();
        RemoveCard();
    }

    public void OnNoButton()
    {
        TestManager.instance.GetNextCard();
        RemoveCard();
    }

    public void RemoveCard()
    {
        Destroy(gameObject);
    }
}
