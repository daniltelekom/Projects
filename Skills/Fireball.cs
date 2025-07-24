using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Fireball : NetworkBehaviour
{
    public float speed = 10f;
    public float lifeTime = 5f;
    public float damage = 20f;
    public GameObject explosionEffect;
    public StatusEffectData statusEffect; // Добавлено

    private float timer;

    private void Start()
    {
        timer = lifeTime;
    }

    private void Update()
    {
        if (!isServer) return;

        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            NetworkServer.Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isServer) return;

        if (other.CompareTag("Enemy"))
        {
            EnemyStats enemy = other.GetComponent<EnemyStats>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                if (statusEffect != null)
                    enemy.ApplyStatusEffect(statusEffect);
            }
        }

        if (explosionEffect != null)
        {
            GameObject effect = Instantiate(explosionEffect, transform.position, Quaternion.identity);
            NetworkServer.Spawn(effect);
            Destroy(effect, 2f);
        }

        NetworkServer.Destroy(gameObject);
    }
}
