using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMissionObjective
{
    void StartObjective(RoomMission context);
    void UpdateObjective();
    bool IsCompleted();
}