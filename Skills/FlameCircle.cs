using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class FlameCircle : NetworkBehaviour
{
    public float damagePerSecond = 10f;
    public float duration = 5f;
    public float radius = 3f;
    public StatusEffectData statusEffect; // Добавлено

    private float timer = 0f;

    private void Update()
    {
        if (!isServer) return;

        timer += Time.deltaTime;
        if (timer >= duration)
        {
            NetworkServer.Destroy(gameObject);
            return;
        }

        Collider[] hits = Physics.OverlapSphere(transform.position, radius);
        foreach (var hit in hits)
        {
            if (hit.TryGetComponent<IDamageable>(out var enemy))
            {
                enemy.TakeDamage(damagePerSecond * Time.deltaTime);
                if (statusEffect != null && enemy is EnemyStats es)
                {
                    es.ApplyStatusEffect(statusEffect);
                }
            }
        }
    }
}