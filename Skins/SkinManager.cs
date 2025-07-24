using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinManager : MonoBehaviour
{
    public static SkinManager Instance;

    public List<SkinData> allSkins;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Сохраняем между сценами, если нужно
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UnlockSkin(string skinName)
    {
        SkinData skin = allSkins.Find(s => s.skinName == skinName);
        if (skin != null)
        {
            skin.isUnlocked = true;
        }
    }

    public SkinData GetSkin(string name)
    {
        return allSkins.Find(s => s.skinName == name);
    }

    public List<SkinData> GetUnlockedSkins()
    {
        return allSkins.FindAll(s => s.isUnlocked);
    }
}