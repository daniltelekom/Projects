using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillUpgradeTerminal : MonoBehaviour
{
    public SkillTreeUI skillUI;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var playerStats = other.GetComponent<PlayerStats>();
            if (playerStats != null)
            {
                skillUI.OpenSkillUpgrade(playerStats);
            }
        }
    }
}