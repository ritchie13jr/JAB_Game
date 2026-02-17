public enum StatType 
{
    StatA, StatB, StatC, StatD
}

[System.Serializable]
public struct StatModifier 
{
    public StatType StatType;
    public float amount;
}