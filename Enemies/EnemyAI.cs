using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Mirror;

public class EnemyAI : NetworkBehaviour
{
    private Transform target;
    private NavMeshAgent agent;
    private EnemyStats stats;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        stats = GetComponent<EnemyStats>();
    }

    private void Update()
    {
        if (!isServer || stats.IsDead) return;

        FindNearestPlayer();

        if (target != null)
        {
            agent.SetDestination(target.position);
        }
    }

    void FindNearestPlayer()
    {
        float closestDist = float.MaxValue;
        Transform closestPlayer = null;

        foreach (var player in GameObject.FindGameObjectsWithTag("Player"))
        {
            float dist = Vector3.Distance(transform.position, player.transform.position);
            if (dist < closestDist)
            {
                closestDist = dist;
                closestPlayer = player.transform;
            }
        }

        target = closestPlayer;
    }
}