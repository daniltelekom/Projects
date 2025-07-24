using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CoopMoneyDistributor : NetworkBehaviour
{
    [Server]
    public void DistributeMoney(int amount)
    {
        foreach (var player in FindObjectsOfType<PlayerStats>())
        {
            player.GetComponent<NetworkIdentity>().connectionToClient?.identity
                .GetComponent<PlayerStats>().AddCurrency(amount);
        }
    }
}