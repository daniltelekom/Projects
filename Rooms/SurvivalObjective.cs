using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivalObjective : MonoBehaviour, IMissionObjective
{
    public float surviveDuration = 30f;
    private float timer = 0f;
    private RoomMission room;
    private bool isCompleted = false;

    public void StartObjective(RoomMission context)
    {
        room = context;
        timer = 0f;
    }

    public void UpdateObjective()
    {
        if (isCompleted) return;

        timer += Time.deltaTime;
        if (timer >= surviveDuration)
        {
            isCompleted = true;
            room.Complete();
        }
    }

    public bool IsCompleted() => isCompleted;
}
