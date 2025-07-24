using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public List<RoomData> allRooms;
    public Transform parent;
    public Vector2 roomSize = new Vector2(16, 9);

    private List<Vector2Int> usedPositions = new();
    private Dictionary<Vector2Int, GameObject> spawnedRooms = new();

    public void Generate()
    {
        usedPositions.Clear();
        spawnedRooms.Clear();

        Vector2Int start = Vector2Int.zero;
        SpawnRoom(start);
    }

    void SpawnRoom(Vector2Int gridPos)
    {
        RoomData roomData = allRooms[Random.Range(0, allRooms.Count)];
        GameObject room = Instantiate(roomData.roomPrefab, parent);
        room.transform.position = new Vector3(gridPos.x * roomSize.x, 0, gridPos.y * roomSize.y);

        usedPositions.Add(gridPos);
        spawnedRooms[gridPos] = room;

        // Пример: добавляем дальше по выходам
        TrySpawnExit(roomData, gridPos, Vector2Int.up, roomData.hasTopExit);
        TrySpawnExit(roomData, gridPos, Vector2Int.down, roomData.hasBottomExit);
        TrySpawnExit(roomData, gridPos, Vector2Int.left, roomData.hasLeftExit);
        TrySpawnExit(roomData, gridPos, Vector2Int.right, roomData.hasRightExit);
    }

    void TrySpawnExit(RoomData from, Vector2Int pos, Vector2Int dir, bool hasExit)
    {
        if (!hasExit) return;
        Vector2Int target = pos + dir;
        if (!usedPositions.Contains(target))
            SpawnRoom(target);
    }
}