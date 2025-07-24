using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoader : MonoBehaviour
{
    public void LoadGame()
    {
        if (PlayerPrefs.HasKey("SaveData"))
        {
            string json = PlayerPrefs.GetString("SaveData");
            PlayerSaveData data = JsonUtility.FromJson<PlayerSaveData>(json);
            GameSaver.ApplyLoadedData(data);
            Debug.Log("Загрузка прошла успешно.");
        }
        else
        {
            Debug.Log("Нет сохранения.");
        }
    }
}