using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitanSkills : NetworkBehaviour
{
    [Header("Shield")]
    public GameObject shieldPrefab;
    public Transform shieldSpawnPoint;

    [Header("Stomp")]
    public GameObject stompEffectPrefab;
    public float stompRadius = 5f;
    public float stompDamage = 30f;

    [Header("Push")]
    public float pushForce = 10f;
    public float pushRange = 3f;

    public void ActivateShield()
    {
        if (!isServer) return;

        GameObject shield = Instantiate(shieldPrefab, shieldSpawnPoint.position, Quaternion.identity);
        NetworkServer.Spawn(shield);
    }

    public void Stomp()
    {
        if (!isServer) return;

        Collider[] hits = Physics.OverlapSphere(transform.position, stompRadius);
        foreach (var hit in hits)
        {
            if (hit.TryGetComponent(out IDamageable enemy))
            {
                enemy.TakeDamage(stompDamage);
            }
        }

        if (stompEffectPrefab)
        {
            GameObject effect = Instantiate(stompEffectPrefab, transform.position, Quaternion.identity);
            NetworkServer.Spawn(effect);
        }
    }

    public void PushBack()
    {
        if (!isServer) return;

        Collider[] hits = Physics.OverlapSphere(transform.position, pushRange);
        foreach (var hit in hits)
        {
            if (hit.TryGetComponent<Rigidbody>(out var rb))
            {
                Vector3 dir = (hit.transform.position - transform.position).normalized;
                rb.AddForce(dir * pushForce, ForceMode.Impulse);
            }
        }
    }
}
