using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CoinPickup : NetworkBehaviour
{
    public int coinValue = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (!isServer) return;

        if (other.CompareTag("Player"))
        {
            // всем игрокам прибавляем монеты
            foreach (GameObject playerObj in GameObject.FindGameObjectsWithTag("Player"))
            {
                var coinUI = playerObj.GetComponent<CoinUI>();
                if (coinUI != null)
                    coinUI.AddCoins(coinValue); // локально на клиенте (не будет работать без команды)

                var playerConn = playerObj.GetComponent<NetworkIdentity>()?.connectionToClient;
                if (playerConn != null)
                    TargetAddCoins(playerConn, coinValue);
            }

            NetworkServer.Destroy(gameObject);
        }
    }

    [TargetRpc]
    void TargetAddCoins(NetworkConnection target, int value)
    {
        CoinUI ui = target.identity.GetComponent<CoinUI>();
        if (ui != null)
        {
            ui.AddCoins(value);
        }
    }
}