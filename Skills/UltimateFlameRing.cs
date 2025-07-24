using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class UltimateFlameRing : NetworkBehaviour
{
    public float duration = 5f;
    public float tickInterval = 0.5f;
    public float damage = 15f;
    public float radius = 5f;
    public LayerMask enemyLayer;
    public StatusEffectData statusEffect; // Добавлено

    private float timer;
    private float tickTimer;

    private void Start()
    {
        timer = duration;
        tickTimer = 0f;
    }

    private void Update()
    {
        if (!isServer) return;

        timer -= Time.deltaTime;
        tickTimer -= Time.deltaTime;

        if (tickTimer <= 0f)
        {
            DealDamage();
            tickTimer = tickInterval;
        }

        if (timer <= 0f)
        {
            NetworkServer.Destroy(gameObject);
        }
    }

    private void DealDamage()
    {
        Collider[] enemies = Physics.OverlapSphere(transform.position, radius, enemyLayer);
        foreach (var enemyCollider in enemies)
        {
            EnemyStats enemy = enemyCollider.GetComponent<EnemyStats>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                if (statusEffect != null)
                    enemy.ApplyStatusEffect(statusEffect);
            }
        }
    }
}