using UnityEngine;
using System.Collections.Generic;

public class CardSpawner : MonoBehaviour
{
    public CardUI cardPrefab;
    public Transform cardContainer;

    public List<CardData> allCards;
    private List<CardData> removedCards = new List<CardData>();

    public CardUI currentCard;

    public void SpawnRandomCard()
    {
        if (allCards.Count == 0) return;
        int randomIndx = Random.Range(0, allCards.Count);

        currentCard = Instantiate(cardPrefab, cardContainer);
        currentCard.Setup(allCards[randomIndx]);
    }

    public void RemoveCard(CardData card)
    {
        if (GameManager.Instance.WinOnAllPublished)
        {
            allCards.Remove(card);
            removedCards.Add(card);
        }
        else
        {
            
        }
    }

    public void Reset()
    {
        allCards.AddRange(removedCards);
        removedCards.Clear();
    }
}
