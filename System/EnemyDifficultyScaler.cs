using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDifficultyScaler : MonoBehaviour
{
    public static int StageLevel = 1;
    public float healthMultiplier = 1.2f;
    public float damageMultiplier = 1.1f;

    public static EnemyDifficultyScaler Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void ScaleEnemyStats(EnemyStats stats)
    {
        stats.maxHealth *= Mathf.Pow(healthMultiplier, StageLevel - 1);
        stats.damage *= Mathf.Pow(damageMultiplier, StageLevel - 1);
        stats.currentHealth = stats.maxHealth;
    }

    public void AdvanceStage()
    {
        StageLevel++;
        Debug.Log("Этап повышен. Новый уровень: " + StageLevel);
    }

    public void ResetDifficulty()
    {
        StageLevel = 1;
    }
}