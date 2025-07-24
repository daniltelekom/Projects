using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinTerminal : MonoBehaviour
{
    public SkinSelectionUI skinUI;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var playerStats = other.GetComponent<PlayerStats>();
            if (playerStats != null)
            {
                skinUI.Open(playerStats.CharacterId);
            }
        }
    }
}