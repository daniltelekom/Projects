using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NanoUpgradeSystem : MonoBehaviour
{
    public List<NanoTechnology> upgrades = new();

    public bool CanUpgrade(NanoTechnology current)
    {
        return upgrades.Exists(t => t.name == current.name && t.level == current.level + 1);
    }
    public NanoTechnology GetNextLevel(NanoTechnology current)
    {
        return upgrades.Find(t => t.name == current.name && t.level == current.level + 1);
    }
}