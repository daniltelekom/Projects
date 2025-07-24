using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliminationObjective : MonoBehaviour, IMissionObjective
{
    private RoomMission room;
    private List<EnemyBase> enemies = new List<EnemyBase>();
    private bool isCompleted = false;

    public void StartObjective(RoomMission context)
    {
        room = context;

        // Собираем всех врагов в этой комнате
        foreach (EnemyBase enemy in GetComponentsInChildren<EnemyBase>())
        {
            enemies.Add(enemy);
            enemy.OnDeath += OnEnemyDeath;
        }

        Debug.Log("EliminationObjective запущена: Врагов найдено " + enemies.Count);
    }

    public void UpdateObjective()
    {
        // Ничего не делаем — всё через события смерти
    }

    public bool IsCompleted()
    {
        return isCompleted;
    }

    private void OnEnemyDeath(EnemyBase deadEnemy)
    {
        if (enemies.Contains(deadEnemy))
        {
            enemies.Remove(deadEnemy);
            Debug.Log("Враг уничтожен! Осталось: " + enemies.Count);
        }

        if (enemies.Count == 0 && !isCompleted)
        {
            isCompleted = true;
            Debug.Log("Все враги уничтожены! Миссия завершена.");
            room.Complete();
        }
    }
}