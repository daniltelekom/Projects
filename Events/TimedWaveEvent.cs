using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class TimedWaveEvent : NetworkBehaviour
{
    public float timeUntilWave = 30f;
    public GameObject[] enemiesToSpawn;
    public Transform[] spawnPoints;

    private bool hasStarted = false;

    public void StartEvent()
    {
        if (!isServer || hasStarted) return;
        hasStarted = true;
        StartCoroutine(SpawnWaveAfterDelay());
    }

    private IEnumerator SpawnWaveAfterDelay()
    {
        yield return new WaitForSeconds(timeUntilWave);
        SpawnWave();
    }

    private void SpawnWave()
    {
        foreach (var point in spawnPoints)
        {
            GameObject enemyPrefab = enemiesToSpawn[Random.Range(0, enemiesToSpawn.Length)];
            GameObject enemy = Instantiate(enemyPrefab, point.position, Quaternion.identity);
            NetworkServer.Spawn(enemy);
        }
    }
}
