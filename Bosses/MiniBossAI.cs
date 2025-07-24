using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBossAI : EnemyBase
{
    public BossAbility specialAbility;
    public float abilityCooldown = 5f;
    private float lastAbilityTime;

    public override void HandleBehavior()
    {
        // Простая логика погони и атаки
        if (aggroTarget != null)
        {
            Vector3 dir = (aggroTarget.position - transform.position).normalized;
            transform.position += dir * speed * Time.deltaTime;

            float distance = Vector3.Distance(transform.position, aggroTarget.position);
            if (distance < 2f && Time.time - lastAttackTime > attackCooldown)
            {
                lastAttackTime = Time.time;
                var stats = aggroTarget.GetComponent<PlayerStats>();
                stats?.TakeDamage(damage);
            }
        }

        // Использование особой способности
        if (Time.time >= lastAbilityTime + abilityCooldown)
        {
            specialAbility?.Activate();
            lastAbilityTime = Time.time;
        }
    }
}