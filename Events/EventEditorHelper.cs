using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(TimedWaveEvent))]
public class EventEditorHelper : Editor
{
    private void OnSceneGUI()
    {
        TimedWaveEvent evt = (TimedWaveEvent)target;

        if (evt.spawnPoints == null) return;

        foreach (var point in evt.spawnPoints)
        {
            if (point == null) continue;
            Handles.color = Color.red;
            Handles.SphereHandleCap(0, point.position, Quaternion.identity, 0.5f, EventType.Repaint);
        }
    }
}
#endif