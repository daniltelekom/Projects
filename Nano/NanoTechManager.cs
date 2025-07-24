using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NanoTechManager : MonoBehaviour
{
    public List<NanoTechnology> allTechnologies = new();

    public List<NanoTechnology> GetRandomTechs(int count, Rarity minRarity = Rarity.Common)
    {
        List<NanoTechnology> filtered = allTechnologies.FindAll(t => t.rarity >= minRarity);
        filtered.Shuffle();
        return filtered.GetRange(0, Mathf.Min(count, filtered.Count));
    }
}