using NUnit.Framework;
using System.Data;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "CardData", menuName = "Scriptable Objects/CardData")]
public class CardData : ScriptableObject
{
    [Header("Card Info")]
    public string title;
    [TextArea]
    public string description;

    [Header("Publish")]
    public List<StatModifier> publishEffects;

}