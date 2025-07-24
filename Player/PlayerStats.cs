using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerStats : NetworkBehaviour
{
    [SyncVar(hook = nameof(OnHealthChanged))]
    public int currentHealth;

    public int maxHealth = 100;

    // 🔧 ДОБАВЛЕННЫЕ СТАТЫ
    public float moneyMultiplier = 1f;
    public float attackBonus = 0f;
    public float fireRateBonus = 0f;

    private float shieldAmount = 0f;

    public event Action<int, int> OnHealthUpdated;
    public event Action OnPlayerDied;

    private bool isDead;
    private PlayerController controller;

    private void Awake()
    {
        controller = GetComponent<PlayerController>();
    }

    private void Start()
    {
        if (isServer)
            currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (!isServer || isDead) return;

        //  Если есть щит, блокируем урон
        if (shieldAmount > 0)
        {
            float shieldAbsorbed = Mathf.Min(shieldAmount, damage);
            shieldAmount -= shieldAbsorbed;
            damage -= Mathf.RoundToInt(shieldAbsorbed);
        }

        if (damage <= 0) return;

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (currentHealth <= 0)
        {
            isDead = true;
            RpcOnDeath();
        }
    }

public void ApplyFireRateBuff(float duration, float multiplier)
{
    StartCoroutine(FireRateBuffRoutine(duration, multiplier));
}

private IEnumerator FireRateBuffRoutine(float duration, float multiplier)
{
    fireRateBonus += multiplier;
    yield return new WaitForSeconds(duration);
    fireRateBonus -= multiplier;
}

    public void AddShield(float amount)
    {
        shieldAmount += amount;
    }

    public void RemoveShield(float amount)
    {
        shieldAmount = Mathf.Max(0f, shieldAmount - amount);
    }

    void OnHealthChanged(int oldHealth, int newHealth)
    {
        OnHealthUpdated?.Invoke(newHealth, maxHealth);
    }

    [ClientRpc]
    void RpcOnDeath()
    {
        Debug.Log("Игрок погиб");

        if (controller != null)
            controller.enabled = false;

        CameraSwitcher.SwitchToNextAlivePlayer(gameObject);
        OnPlayerDied?.Invoke();
    }

public int money = 0;

public void AddCurrency(int amount)
{
    money += Mathf.Max(0, amount); // Не даём отрицательного бабла
}
}