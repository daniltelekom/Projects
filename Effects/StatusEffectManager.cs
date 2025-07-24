using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffectManager : MonoBehaviour
{
    private Dictionary<StatusEffectType, StatusEffectInstance> activeEffects = new();

    // --- СОБЫТИЯ ---
    public delegate void StatusEffectApplied(StatusEffectType type, StatusEffectInstance instance);
    public delegate void StatusEffectRemoved(StatusEffectType type);
    public event StatusEffectApplied OnEffectApplied;
    public event StatusEffectRemoved OnEffectRemoved;

    // --- ПРИМЕНЕНИЕ ЭФФЕКТА (по data) ---
    public void ApplyEffect(StatusEffectData data)
    {
        var instance = new StatusEffectInstance(data);

        if (activeEffects.ContainsKey(data.effectType))
            activeEffects[data.effectType] = instance;
        else
            activeEffects.Add(data.effectType, instance);

        OnEffectApplied?.Invoke(data.effectType, instance);

        if (data.effectVFX)
            Instantiate(data.effectVFX, transform.position, Quaternion.identity, transform);
    }

    // --- ПРИМЕНЕНИЕ ЭФФЕКТА (по параметрам) ---
    public void ApplyStatus(StatusEffectType type, float duration, float value = 0)
    {
        StatusEffectData data = ScriptableObject.CreateInstance<StatusEffectData>();
        data.effectType = type;
        data.duration = duration;
        data.value = value;
        data.effectName = type.ToString();
        ApplyEffect(data);
    }

    // --- ПРОВЕРКИ ---
    public bool HasEffect(StatusEffectType type) => activeEffects.ContainsKey(type);

    public float GetEffectValue(StatusEffectType type)
        => activeEffects.TryGetValue(type, out var instance) ? instance.effectData.value : 0f;

    public StatusEffectInstance GetEffect(StatusEffectType type)
        => activeEffects.TryGetValue(type, out var instance) ? instance : null;

    // --- ОБНОВЛЕНИЕ ЭФФЕКТОВ ---
    private void Update()
    {
        float delta = Time.deltaTime;
        List<StatusEffectType> toRemove = new();

        foreach (var kvp in activeEffects)
        {
            kvp.Value.Tick(gameObject, delta);
            if (kvp.Value.IsExpired())
                toRemove.Add(kvp.Key);
        }

        foreach (var type in toRemove)
        {
            activeEffects.Remove(type);
            OnEffectRemoved?.Invoke(type);
        }
    }
}