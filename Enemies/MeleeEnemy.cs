using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : EnemyBase
{
    public float attackRange = 2f;
    public float attackCooldown = 1.5f;
    public int damage = 10;
    private float lastAttackTime;

    public override void HandleBehavior()
    {
        Transform target = GetClosestPlayer();
        if (target != null && Vector3.Distance(transform.position, target.position) <= attackRange)
        {
            if (Time.time >= lastAttackTime + attackCooldown)
            {
                target.GetComponent<PlayerStats>()?.TakeDamage(damage);
                lastAttackTime = Time.time;
            }
        }
    }

    Transform GetClosestPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        float minDist = float.MaxValue;
        Transform closest = null;

        foreach (GameObject player in players)
        {
            float dist = Vector3.Distance(transform.position, player.transform.position);
            if (dist < minDist)
            {
                closest = player.transform;
                minDist = dist;
            }
        }

        return closest;
    }
}