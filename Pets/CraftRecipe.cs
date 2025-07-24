using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class CraftRecipe
{
    public ResourceAmount[] resources;
    public int moneyCost;
    public string blueprintID;
}

[System.Serializable]
public struct ResourceAmount
{
    public string resourceName;
    public int amount;
}