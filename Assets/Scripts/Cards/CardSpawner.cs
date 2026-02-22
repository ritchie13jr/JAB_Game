using UnityEngine;
using System.Collections.Generic;
using Mono.Cecil;

public class CardSpawner : MonoBehaviour
{
    public CardUI cardPrefab;
    public Transform cardContainer;

    public List<CardData> allCards;

    public void SpawnRandomCard() 
    {
        int randomIndx = Random.Range(0, allCards.Count);

        CardUI newCard = Instantiate(cardPrefab, cardContainer);
        newCard.Setup(allCards[randomIndx]);
    }
}
