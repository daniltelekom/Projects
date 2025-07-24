using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomConnector : MonoBehaviour
{
    public enum Direction { Top, Bottom, Left, Right }
    public Direction direction;
    public bool isConnected;
}