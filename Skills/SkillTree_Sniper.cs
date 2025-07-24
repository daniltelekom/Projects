using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillTree_Sniper", menuName = "SkillTree/Sniper")]
public class SkillTree_Sniper : ScriptableObject
{
    public List<SkillNodeData> nodes = new List<SkillNodeData>
    {
        //  Центровой узел
        new SkillNodeData
        {
            id = "SniperCore",
            name = "Хладнокровие",
            description = "Критический урон +10%",
            type = SkillNodeType.Core,
            requiredIds = new List<string>(),
            percentBonus = 10f,
            targetStat = PlayerStatType.CritDamage
        },

        //  Пассивки
        new SkillNodeData
        {
            id = "CritChanceUp",
            name = "Меткость",
            description = "Шанс крита +5%",
            type = SkillNodeType.Passive,
            requiredIds = new List<string> { "SniperCore" },
            percentBonus = 5f,
            targetStat = PlayerStatType.CritChance
        },
        new SkillNodeData
        {
            id = "CooldownReduction",
            name = "Проворство",
            description = "Скиллы перезаряжаются быстрее на 10%",
            type = SkillNodeType.Passive,
            requiredIds = new List<string> { "SniperCore" },
            percentBonus = 10f,
            targetStat = PlayerStatType.SkillCooldown
        },
        new SkillNodeData
        {
            id = "SkillDamageUp",
            name = "Фокусировка",
            description = "Урон умений +15%",
            type = SkillNodeType.Passive,
            requiredIds = new List<string> { "CritChanceUp" },
            percentBonus = 15f,
            targetStat = PlayerStatType.SkillPower
        },
        new SkillNodeData
        {
            id = "FireRateBoost",
            name = "Скоростной огонь",
            description = "Скорость стрельбы +10%",
            type = SkillNodeType.Passive,
            requiredIds = new List<string> { "CooldownReduction" },
            percentBonus = 10f,
            targetStat = PlayerStatType.FireRate
        },

        //  Активки
        new SkillNodeData
        {
            id = "SnipeMastery",
            name = "Снайперская точность",
            description = "Выстрел пробивает 2 врага и замедляет при критах",
            type = SkillNodeType.ActiveUpgrade,
            requiredIds = new List<string> { "SkillDamageUp" },
            modifiesSkill = SkillID.Snipe
        },
        new SkillNodeData
        {
            id = "DashEvadeBoost",
            name = "Тактический отход",
            description = "После рывка уклонение +20% и +1 заряд рывка",
            type = SkillNodeType.ActiveUpgrade,
            requiredIds = new List<string> { "FireRateBoost" },
            modifiesSkill = SkillID.Dash
        },
        new SkillNodeData
        {
            id = "BurstShotUpgrade",
            name = "Тройной выстрел",
            description = "3 быстрых выстрела, каждый с +10% шансом крита",
            type = SkillNodeType.ActiveUpgrade,
            requiredIds = new List<string> { "SnipeMastery" },
            modifiesSkill = SkillID.BurstShot
        },
        new SkillNodeData
        {
            id = "UltimateOverkill",
            name = "Контроль выстрела",
            description = "Ульта критует по врагам с <30% HP и бьёт по площади",
            type = SkillNodeType.ActiveUpgrade,
            requiredIds = new List<string> { "DashEvadeBoost" },
            modifiesSkill = SkillID.Ultimate
        }
    };
}
