using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class DataTransferObjective : NetworkBehaviour, IMissionObjective
{
    public float transferTime = 10f;
    private float progress = 0f;

    private bool playerInside = false;
    private bool isCompleted = false;

    private RoomMission roomMission;

    public void StartObjective(RoomMission context)
    {
        roomMission = context;
        progress = 0f;
        isCompleted = false;
        // Тут можно активировать визуал, звук, индикаторы
    }

    public void UpdateObjective()
    {
        if (!isServer || isCompleted) return;

        if (playerInside)
        {
            progress += Time.deltaTime;
            if (progress >= transferTime)
            {
                isCompleted = true;
                ObjectiveComplete();
            }
        }
    }

    public bool IsCompleted() => isCompleted;

    private void ObjectiveComplete()
    {
        Debug.Log(" Data transfer completed!");
        roomMission?.Complete();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isServer) return;

        if (other.CompareTag("Player"))
        {
            playerInside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!isServer) return;

        if (other.CompareTag("Player"))
        {
            playerInside = false;
        }
    }
}
