using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetInstance : MonoBehaviour
{
    public PetData petData;
    private PlayerStats playerStats;

    private void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        ApplyBuffs();
    }

    private void OnDestroy()
    {
        RemoveBuffs();
    }

    private void ApplyBuffs()
    {
        if (petData == null || playerStats == null) return;

        switch (petData.petType)
        {
            case PetType.Turtle:
                playerStats.AddShield(petData.shieldAmount);
                break;
            case PetType.Rat:
                playerStats.moneyMultiplier += petData.moneyMultiplier;
                break;
            case PetType.Dog:
                playerStats.attackBonus += petData.attackBonus;
                break;
            case PetType.Ferret:
                playerStats.fireRateBonus += petData.fireRateBonus;
                break;
        }
    }

    private void RemoveBuffs()
    {
        if (petData == null || playerStats == null) return;

        switch (petData.petType)
        {
            case PetType.Turtle:
                playerStats.RemoveShield(petData.shieldAmount);
                break;
            case PetType.Rat:
                playerStats.moneyMultiplier -= petData.moneyMultiplier;
                break;
            case PetType.Dog:
                playerStats.attackBonus -= petData.attackBonus;
                break;
            case PetType.Ferret:
                playerStats.fireRateBonus -= petData.fireRateBonus;
                break;
        }
    }

    public void Upgrade()
    {
        if (petData.level < petData.maxLevel)
        {
            RemoveBuffs();
            petData.level++;
            ApplyBuffs();
        }
    }
}