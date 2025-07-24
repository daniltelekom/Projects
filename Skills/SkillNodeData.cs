using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SkillNodeData
{
    public string id; // Уникальный ID узла
    public string name; // Название для UI
    public string description; // Описание для UI

    public SkillNodeType type; // Core / Passive / ActiveUpgrade
    public List<string> requiredIds = new(); // Какие ноды обязательны до разблокировки

    // Пассивка
    public float percentBonus; // Насколько увеличивается стат
    public PlayerStatType targetStat; // Какой именно стат

    // Активка
    public SkillID modifiesSkill; // Какой скилл усиливает (если ActiveUpgrade)
}