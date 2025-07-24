using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Mirror;

using UnityEngine;
using System.Collections.Generic;

public class EnemyPoolManager : MonoBehaviour
{
    public static EnemyPoolManager Instance;

    public GameObject enemyPrefab;
    public int initialPoolSize = 10;

    private Queue<GameObject> pool = new Queue<GameObject>();

    void Awake()
    {
        Instance = this;
        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject obj = Instantiate(enemyPrefab);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    public GameObject GetEnemy(Vector3 position, Quaternion rotation)
    {
        GameObject enemy;
        if (pool.Count > 0)
        {
            enemy = pool.Dequeue();
        }
        else
        {
            enemy = Instantiate(enemyPrefab);
        }

        enemy.transform.position = position;
        enemy.transform.rotation = rotation;
        enemy.SetActive(true);
        return enemy;
    }

    public void ReturnToPool(GameObject enemy)
    {
        enemy.SetActive(false);
        pool.Enqueue(enemy);
    }
}