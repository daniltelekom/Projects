using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPhaseController : MonoBehaviour
{
    public float[] phaseThresholds; // в процентах HP: 0.7, 0.4, 0.2 и т.д.
    public BossAbility[] phaseAbilities;
    private EnemyStats stats;
    private int currentPhase = -1;

    void Start()
    {
        stats = GetComponent<EnemyStats>();
        UpdatePhase(); // сразу активируем первую фазу
    }

    public void UpdatePhase()
    {
        float hpPercent = stats.CurrentHealth / stats.MaxHealth;

        for (int i = 0; i < phaseThresholds.Length; i++)
        {
            if (hpPercent <= phaseThresholds[i] && currentPhase < i)
            {
                currentPhase = i;
                phaseAbilities[i].Activate();
            }
        }
    }
}