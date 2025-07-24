using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class NanoSaver
{
    private static string SavePath => Path.Combine(Application.persistentDataPath, "nano_save.json");

    public static void Save(NanoTechSaveData data)
    {
        File.WriteAllText(SavePath, JsonUtility.ToJson(data));
    }

    public static NanoTechSaveData Load()
    {
        if (File.Exists(SavePath))
            return JsonUtility.FromJson<NanoTechSaveData>(File.ReadAllText(SavePath));
        return new NanoTechSaveData();
    }
}

[System.Serializable]
public class NanoTechSaveData
{
    public List<NanoTechnology> savedTechs = new();
}