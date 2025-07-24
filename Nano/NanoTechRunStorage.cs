using System.Collections.Generic;
using UnityEngine;

public class NanoTechRunStorage : MonoBehaviour
{
    public List<NanoTechnology> temporaryTechs = new();

    public void AddTechnology(NanoTechnology tech)
    {
        NanoTechnology clone = new NanoTechnology(tech); // создаём копию
        temporaryTechs.Add(clone);
    }

    public void ClearAll()
    {
        temporaryTechs.Clear();
    }

    public List<NanoTechnology> GetAll()
    {
        return temporaryTechs;
    }

    public bool HasTechnology(string name)
    {
        return temporaryTechs.Exists(t => t.name == name);
    }
}