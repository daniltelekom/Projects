using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSaver : MonoBehaviour
{
    public static PlayerSaveData currentSave = new PlayerSaveData();

    public static void SaveGame()
    {
        string json = JsonUtility.ToJson(currentSave);
        PlayerPrefs.SetString("SaveData", json);
        PlayerPrefs.Save();
        Debug.Log("Игра сохранена.");
    }

    public static void ApplyLoadedData(PlayerSaveData data)
    {
        currentSave = data;
        // TODO: применить сохранённые данные (скины, технологии, питомцы и т.д.)
    }
}