using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTreeManager : MonoBehaviour
{
    private Dictionary<string, float> skillBonuses = new();

    // Добавить бонус скиллу
    public void SetSkillBonus(string skillName, float value)
    {
        skillBonuses[skillName] = value;
    }

    // Получить бонус (если не задан, вернёт 0)
    public float GetSkillBonus(string skillName)
    {
        if (skillBonuses.TryGetValue(skillName, out float bonus))
            return bonus;
        return 0f;
    }
}