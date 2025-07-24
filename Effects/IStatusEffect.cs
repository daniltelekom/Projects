using UnityEngine;

public interface IStatusEffect
{
    void ApplyEffect(GameObject target);
    void RemoveEffect(GameObject target);
    void TickEffect(GameObject target, float deltaTime);
}

public class TickingEffect : IStatusEffect
{
    private StatusEffectData data;
    private float elapsed = 0f;
    private float durationRemaining;

    public TickingEffect(StatusEffectData data)
    {
        this.data = data;
        this.durationRemaining = data.duration;
    }

    public void ApplyEffect(GameObject target)
    {
        // Можно повесить VFX сразу
        if (data.effectVFX)
        {
            Object.Instantiate(data.effectVFX, target.transform.position, Quaternion.identity, target.transform);
        }
    }

    public void RemoveEffect(GameObject target)
    {
        // Здесь можно удалить VFX или эффект вручную, если надо
    }

    public void TickEffect(GameObject target, float deltaTime)
    {
        if (durationRemaining <= 0f) return;

        elapsed += deltaTime;
        durationRemaining -= deltaTime;

        if (elapsed >= data.tickInterval)
        {
            elapsed = 0f;
            StatusEffectLibrary.ApplyTickEffect(data, target);
        }
    }

    public bool IsExpired()
    {
        return durationRemaining <= 0f;
    }
}