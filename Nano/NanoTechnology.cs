using System;

[Serializable]
public class NanoTechnology
{
    public string id;
    public string name;
    public string description;
    public Rarity rarity;
    public int level;
    public string effectId; // ID эффекта

    public NanoTechnology(NanoTechnology original)
    {
        id = original.id;
        name = original.name;
        description = original.description;
        rarity = original.rarity;
        level = original.level;
        effectId = original.effectId;
    }
}

public enum Rarity
{
    Common,    // серый
    Rare,      // синий
    Epic,      // золотой
    Legendary  // фиолетовый
}
public NanoTechnology CloneWithLevelUp()
{
    NanoTechnology clone = new NanoTechnology(this);
    clone.level += 1;
    return clone;
}