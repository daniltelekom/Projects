using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerSaveData
{
    public string selectedSkinId;
    public int characterLevel;
    public List<string> equippedNanoTechs = new();
    public Dictionary<string, int> skillLevels = new();
    public List<string> ownedSkins = new();
    public List<string> ownedPets = new();
    public Dictionary<string, int> petLevels = new();

    // Добавлять новые поля по мере добавления фич
}