using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StatusEffectLibrary
{
    public static IStatusEffect CreateEffect(StatusEffectData data)
    {
        return new TickingEffect(data);
    }

    public static void ApplyTickEffect(StatusEffectData data, GameObject target)
    {
        var stats = target.GetComponent<EnemyStats>();
        if (stats == null) return;

        switch (data.effectType)
        {
            case StatusEffectType.Poison:
            case StatusEffectType.Burn:
                stats.TakeDamage(data.value);
                break;
            case StatusEffectType.Slow:
                stats.ApplySlow(data.value, data.duration);
                break;
            case StatusEffectType.Stun:
                stats.Stun(data.duration);
                break;
            case StatusEffectType.Freeze:
                stats.Freeze(data.duration);
                break;
        }
    }
}
