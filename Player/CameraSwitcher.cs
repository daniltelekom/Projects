using Mirror;
using Mirror.Examples.Common.Controllers.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public static void SwitchToNextAlivePlayer(GameObject deadPlayer)
    {
        foreach (NetworkIdentity identity in NetworkServer.connections.Values)
        {
            GameObject obj = identity.identity?.gameObject;
            if (obj == null || obj == deadPlayer) continue;

            PlayerStats stats = obj.GetComponent<PlayerStats>();
            if (stats != null && stats.currentHealth > 0)
            {
                Camera deadCam = deadPlayer.GetComponent<PlayerController>()?.cam;
                Camera targetCam = obj.GetComponent<PlayerController>()?.cam;

                if (deadCam != null) deadCam.gameObject.SetActive(false);
                if (targetCam != null) targetCam.gameObject.SetActive(true);

                break;
            }
        }
    }
}