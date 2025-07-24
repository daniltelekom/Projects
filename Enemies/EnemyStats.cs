using UnityEngine;

[RequireComponent(typeof(StatusEffectManager))]
[RequireComponent(typeof(EnemyBase))]
public class EnemyStats : MonoBehaviour
{
    [Header("Статы")]
    public float maxHealth = 100f;
    public float currentHealth;
    public float damage = 10f;

    [Header("Скорость")]
    public float baseMoveSpeed = 3f;
    private float currentMoveSpeed;

    private StatusEffectManager effectManager;
    private EnemyBase baseScript;

    private void Awake()
    {
        effectManager = GetComponent<StatusEffectManager>();
        baseScript = GetComponent<EnemyBase>();
        currentMoveSpeed = baseMoveSpeed;
        currentHealth = maxHealth;

        effectManager.OnEffectApplied += HandleEffectApplied;
        effectManager.OnEffectRemoved += HandleEffectRemoved;
    }

    private void OnEnable()
    {
        ResetStats(); // Сброс при повторном использовании из пула
    }

    private void OnDestroy()
    {
        if (effectManager != null)
        {
            effectManager.OnEffectApplied -= HandleEffectApplied;
            effectManager.OnEffectRemoved -= HandleEffectRemoved;
        }
    }

    private void HandleEffectApplied(StatusEffectType type, StatusEffectInstance instance)
    {
        switch (type)
        {
            case StatusEffectType.Slow:
                currentMoveSpeed = baseMoveSpeed * (1f - instance.data.value);
                break;
            case StatusEffectType.Stun:
            case StatusEffectType.Freeze:
                currentMoveSpeed = 0f;
                break;
        }
    }

    private void HandleEffectRemoved(StatusEffectType type)
    {
        if (!effectManager.HasEffect(StatusEffectType.Slow) &&
            !effectManager.HasEffect(StatusEffectType.Stun) &&
            !effectManager.HasEffect(StatusEffectType.Freeze))
        {
            currentMoveSpeed = baseMoveSpeed;
        }
        else if (effectManager.HasEffect(StatusEffectType.Slow))
        {
            var slow = effectManager.GetEffect(StatusEffectType.Slow);
            currentMoveSpeed = baseMoveSpeed * (1f - slow.data.value);
        }
        else
        {
            currentMoveSpeed = 0f;
        }
    }

    public float GetMoveSpeed() => currentMoveSpeed;
    public bool IsDead => currentHealth <= 0f;

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        baseScript.OnDeath(); // пусть EnemyBase сам решает: деактивировать, сыграть анимацию и т.д.
    }

    public void ResetStats()
    {
        currentHealth = maxHealth;
        currentMoveSpeed = baseMoveSpeed;
        // Также можно обнулить баффы и эффекты
    }
}