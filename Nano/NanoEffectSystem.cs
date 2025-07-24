using System.Collections.Generic;
using UnityEngine;

public class NanoEffectSystem : MonoBehaviour
{
    public List<StatusEffectInstance> activeEffects = new();

    public void ApplyEffect(StatusEffectData effectData)
    {
        var instance = new StatusEffectInstance(effectData);
        activeEffects.Add(instance);

        if (effectData.effectVFX != null)
        {
            Instantiate(effectData.effectVFX, transform.position, Quaternion.identity, transform);
        }
    }

    public void RemoveEffect(StatusEffectData data)
    {
        activeEffects.RemoveAll(e => e.effectData == data);
    }

    public void ClearAllEffects()
    {
        activeEffects.Clear();
    }

    private void Update()
    {
        float delta = Time.deltaTime;
        List<StatusEffectInstance> toRemove = new();

        foreach (var effect in activeEffects)
        {
            effect.Tick(gameObject, delta);
            if (effect.IsExpired())
                toRemove.Add(effect);
        }
foreach (var expired in toRemove)
        {
            activeEffects.Remove(expired);
        }
    }
}