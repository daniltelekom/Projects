using UnityEngine;

public class StatusEffectInstance
{
    public StatusEffectData effectData;
    public float remainingDuration;
    private float tickTimer;

    public StatusEffectInstance(StatusEffectData data)
    {
        effectData = data;
        remainingDuration = data.duration;
        tickTimer = 0f;
    }

    public void Tick(GameObject target, float deltaTime)
    {
        remainingDuration -= deltaTime;
        tickTimer += deltaTime;

        if (tickTimer >= effectData.tickInterval)
        {
            StatusEffectLibrary.ApplyTickEffect(effectData, target);
            tickTimer = 0f;
        }
    }

    public bool IsExpired() => remainingDuration <= 0f;
}