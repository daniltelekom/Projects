using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class EnemySpawner : NetworkBehaviour
{
    public GameObject[] enemyPrefabs;
    public Transform[] spawnPoints;

    public void SpawnEnemies(int count)
    {
        if (!isServer) return;

        for (int i = 0; i < count; i++)
        {
            int randomIndex = Random.Range(0, enemyPrefabs.Length);
            Transform point = spawnPoints[Random.Range(0, spawnPoints.Length)];

            GameObject enemy = Instantiate(enemyPrefabs[randomIndex], point.position, Quaternion.identity);
            NetworkServer.Spawn(enemy);
        }
    }
}