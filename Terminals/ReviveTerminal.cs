using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReviveTerminal : MonoBehaviour
{
    public int reviveCost = 200;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        var players = GameObject.FindGameObjectsWithTag("Player");
        foreach (var p in players)
        {
            var stats = p.GetComponent<PlayerStats>();
            if (stats != null && stats.IsDead)
            {
                var reviver = other.GetComponent<PlayerStats>();
                if (reviver.Money >= reviveCost)
                {
                    reviver.Money -= reviveCost;
                    stats.Revive();
                    Debug.Log($"{reviver.name} воскресил {p.name}");
                }
                else
                {
                    Debug.Log("Недостаточно средств для воскрешения");
                }
            }
        }
    }
}