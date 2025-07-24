#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class RoomEditorHelper : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(16, 1, 9)); // размер можно менять

        foreach (RoomConnector connector in GetComponentsInChildren<RoomConnector>())
        {
            Vector3 dir = Vector3.zero;
            switch (connector.direction)
            {
                case RoomConnector.Direction.Top: dir = Vector3.forward; break;
                case RoomConnector.Direction.Bottom: dir = Vector3.back; break;
                case RoomConnector.Direction.Left: dir = Vector3.left; break;
                case RoomConnector.Direction.Right: dir = Vector3.right; break;
            }

            Gizmos.color = connector.isConnected ? Color.blue : Color.red;
            Gizmos.DrawLine(connector.transform.position, connector.transform.position + dir * 2);
        }
    }
}
#endif