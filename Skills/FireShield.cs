using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class FireShield : NetworkBehaviour
{
    public float duration = 10f;
    public float damageInterval = 1f;
    public float damageAmount = 5f;

    private float timer;
    private float damageTimer;

    private void Start()
    {
        timer = duration;
        damageTimer = damageInterval;
    }

    private void Update()
    {
        if (!isServer) return;

        timer -= Time.deltaTime;
        damageTimer -= Time.deltaTime;

        if (timer <= 0f)
        {
            NetworkServer.Destroy(gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!isServer) return;

        if (damageTimer > 0f) return;

        if (other.CompareTag("Enemy"))
        {
            EnemyStats enemy = other.GetComponent<EnemyStats>();
            if (enemy != null)
            {
                enemy.TakeDamage(damageAmount);
                damageTimer = damageInterval;
            }
        }
    }
}