using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public abstract class EnemyBase : NetworkBehaviour, IDamageable
{
    public float maxHP = 100f;

    [SyncVar(hook = nameof(OnHPChanged))]
    public float currentHP;

    [SyncVar]
    public float damage = 10f;

    public float speed = 3f;
    public float attackCooldown = 1f;

    protected float lastAttackTime;
    protected Transform aggroTarget;
    protected float aggroTimer;

    protected StatusEffectManager effectManager;

    public event System.Action<EnemyBase> OnDeath;

    private bool isDead = false;

    public override void OnStartServer()
    {
        currentHP = maxHP;
        isDead = false;
    }

    public virtual void Start()
    {
        effectManager = GetComponent<StatusEffectManager>();
    }

    public virtual void Update()
    {
        if (!isServer || isDead) return;

        if (aggroTimer > 0)
            aggroTimer -= Time.deltaTime;
        else
            aggroTarget = null;

        HandleBehavior();
    }

    public abstract void HandleBehavior();

    [Server]
    public void TakeDamage(float dmg, GameObject source = null)
    {
        if (isDead) return;

        if (effectManager != null && effectManager.HasEffect(StatusEffectType.ReflectDamage) && source != null)
        {
            float reflectPercent = effectManager.GetEffectValue(StatusEffectType.ReflectDamage);
            source.GetComponent<PlayerStats>()?.TakeDamage(dmg * reflectPercent);
        }

        currentHP -= dmg;

        if (currentHP <= 0)
            Die();
    }

    [Server]
    void IDamageable.TakeDamage(float amount)
    {
        TakeDamage(amount, null);
    }

    void OnHPChanged(float oldVal, float newVal)
    {
        //  Визуал HP-бара можно воткнуть тут
    }

   [Server]
protected override void Die()
{
    OnDeath?.Invoke(this);
    EnemyPoolManager.Instance.ReturnToPool(gameObject); //  вместо Destroy
}

    [Server]
    public void ApplyStatus(StatusEffectType type, float duration, float value = 0)
    {
        effectManager?.ApplyStatus(type, duration, value);
    }

    [Server]
    public void ForceAggro(Transform player, float duration)
    {
        aggroTarget = player;
        aggroTimer = duration;
    }

    //  Вызывай при спауне из пула
    [Server]
    public void ResetEnemy()
    {
        currentHP = maxHP;
        isDead = false;
        aggroTarget = null;
        aggroTimer = 0f;
        lastAttackTime = 0f;

        effectManager?.ClearAll(); // Добавь такой метод если хочешь сбрасывать эффекты
    }
}