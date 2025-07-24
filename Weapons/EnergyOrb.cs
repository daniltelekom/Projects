using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyOrb : MonoBehaviour
{
    public float speed = 10f;
    public float damage = 15f;
    public float explosionRadius = 5f;
    public float effectDuration = 3f;
    public StatusEffectType effectToApply = StatusEffectType.Burn;
    public float lifeTime = 4f;

    private float timer;

    private void OnEnable()
    {
        timer = 0f;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if (timer >= lifeTime)
            Explode();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Explode();
        }
    }

    void Explode()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                var enemy = hit.GetComponent<EnemyBase>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                    enemy.ApplyStatus(effectToApply, effectDuration);
                }
            }
        }

        // Тут можно добавить эффект взрыва
        gameObject.SetActive(false);
    }
}