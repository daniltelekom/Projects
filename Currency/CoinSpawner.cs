using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab;
    public int coinsToDrop = 5;

    public void SpawnCoins(Vector3 position)
    {
        for (int i = 0; i < coinsToDrop; i++)
        {
            Vector3 offset = Random.insideUnitSphere * 1.5f;
            offset.y = 0f;
            Instantiate(coinPrefab, position + offset, Quaternion.identity);
        }
    }
}