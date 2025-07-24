using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Pets/Pet Data")]
public class PetData : ScriptableObject
{
    public string petName;
    public GameObject prefab;
    public Sprite icon;
    public string description;

    public PetType petType; // enum PetType { Turtle, Rat, Dog, Ferret }

    public int maxLevel = 5;

    // Бонусы по типу
    public float shieldAmount;      // Для Turtle
    public float moneyMultiplier;   // Для Rat
    public float attackBonus;       // Для Dog
    public float fireRateBonus;     // Для Ferret

    public CraftRecipe craftRecipe;

    public PetBonus[] bonuses;

    // Временно храним текущий уровень — можно вынести в отдельную сущность PetInstance
    [HideInInspector] public int level = 1;

    public string GetNextLevelBonusInfo()
    {
        if (level >= maxLevel) return "Max Level";
        string info = "";
        foreach (var b in bonuses)
        {
            float nextVal = (level + 1) * b.valuePerLevel;
            info += $"- {b.statDescription}: +{nextVal}\n";
        }
        return info;
    }

    public string GetRequiredMaterialsInfo()
    {
        string result = "";
        foreach (var r in craftRecipe.resources)
        {
            result += $"{r.resourceName}: {r.amount}\n";
        }
        result += $"Money: {craftRecipe.moneyCost}";
        return result;
    }
    [System.Serializable]
    public struct PetBonus
    {
        public string statDescription;
        public float valuePerLevel;
    }
}