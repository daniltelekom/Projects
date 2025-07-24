using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : EnemyBase
{
    public GameObject projectilePrefab;
    public Transform shootPoint;
    public float attackRange = 10f;
    public float cooldown = 2f;
    public float projectileSpeed = 15f;
    private float lastShotTime;

    public override void HandleBehavior()
    {
        Transform target = GetClosestPlayer();
        if (target != null && Vector3.Distance(transform.position, target.position) <= attackRange)
        {
            if (Time.time >= lastShotTime + cooldown)
            {
                Shoot(target.position);
                lastShotTime = Time.time;
            }
        }
    }

    void Shoot(Vector3 targetPos)
    {
        GameObject proj = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);
        proj.GetComponent<Rigidbody>().velocity = (targetPos - shootPoint.position).normalized * projectileSpeed;
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