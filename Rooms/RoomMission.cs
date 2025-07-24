using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomMission : MonoBehaviour
{
    public GameObject[] objectivePrefabs;

    private IMissionObjective currentObjective;
    private bool isCompleted = false;

    private void Start()
    {
        // Пример: выбираем рандомную миссию
        GameObject obj = Instantiate(objectivePrefabs[Random.Range(0, objectivePrefabs.Length)], transform);
        currentObjective = obj.GetComponent<IMissionObjective>();
        currentObjective.StartObjective(this);
    }

    private void Update()
    {
        if (!isCompleted)
            currentObjective.UpdateObjective();
    }
    public void Complete()
    {
        if (!isCompleted)
        {
            isCompleted = true;
            Debug.Log("Миссия завершена!");
            // Открытие дверей, активация терминалов и т.д.
        }
    }
}