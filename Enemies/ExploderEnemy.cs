using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploderEnemy : EnemyBase
{
    public float explosionRadius = 5f;
    public float explosionDamage = 50f;
    public GameObject explosionEffect;

    public override void HandleBehavior()
    {
        Transform target = FindClosestPlayer();
        if (target != null && Vector3.Distance(transform.position, target.position) <= explosionRadius)
        {
            Explode();
        }
    }

    void Explode()
    {
        if (explosionEffect != null)
            Instantiate(explosionEffect, transform.position, Quaternion.identity);

        Collider[] hits = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                PlayerStats stats = hit.GetComponent<PlayerStats>();
                if (stats != null)
                {
                    stats.TakeDamage((int)explosionDamage);
                }
            }
        }

        NetworkServer.Destroy(gameObject); // если на сервере, а не просто Destroy
    }

    Transform FindClosestPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        Transform closest = null;
        float minDistance = Mathf.Infinity;

        foreach (var p in players)
        {
            float dist = Vector3.Distance(transform.position, p.transform.position);
            if (dist < minDistance)
            {
                minDistance = dist;
                closest = p.transform;
            }
        }

        return closest;
    }
}