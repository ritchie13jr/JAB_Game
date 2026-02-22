using UnityEngine;
using System.Collections.Generic;

public class CardSpawner : MonoBehaviour
{
    public CardUI cardPrefab;
    public Transform cardContainer;

    public List<CardData> allCards;

    public CardUI currentCard;

    public void SpawnRandomCard()
    {
        int randomIndx = Random.Range(0, allCards.Count);

        currentCard = Instantiate(cardPrefab, cardContainer);
        currentCard.Setup(allCards[randomIndx]);
    }
}
