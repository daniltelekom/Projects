using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : EnemyBase
{
    public BossPhaseController phaseController;

    public override void HandleBehavior()
    {
        // Обновляем текущую фазу босса
        phaseController?.UpdatePhase();

        // Здесь можно вставить кастомную логику босса:
        // Например, атаки, движение к цели, смена поведения
        if (aggroTarget != null)
        {
            Vector3 direction = (aggroTarget.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;

            // Пример простейшей атаки
            float distance = Vector3.Distance(transform.position, aggroTarget.position);
            if (distance < 2f && Time.time - lastAttackTime > attackCooldown)
            {
                lastAttackTime = Time.time;
                var targetStats = aggroTarget.GetComponent<PlayerStats>();
                if (targetStats != null)
                {
                    targetStats.TakeDamage(damage);
                }
            }
        }
    }
}