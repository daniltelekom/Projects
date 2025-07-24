using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyDropManager : MonoBehaviour
{
    public GameObject moneyPrefab;
    public int moneyAmount = 5;
    public float scatterRadius = 1f;

    public void DropMoney(Vector3 position)
    {
        for (int i = 0; i < moneyAmount; i++)
        {
            Vector3 offset = Random.insideUnitSphere * scatterRadius;
            offset.y = 0;
            Instantiate(moneyPrefab, position + offset, Quaternion.identity);
        }
    }
}