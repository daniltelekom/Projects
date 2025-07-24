using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewRoomData", menuName = "Rooms/RoomData")]
public class RoomData : ScriptableObject
{
    public GameObject roomPrefab;
    public bool hasTopExit;
    public bool hasBottomExit;
    public bool hasLeftExit;
    public bool hasRightExit;

    public Vector2Int size = new Vector2Int(1, 1); // Для G/T-образных комнат
}