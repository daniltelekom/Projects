using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : NetworkBehaviour
{
    public class SyncInventory : SyncDictionary<int, int> { }
    public SyncInventory inventoryItems = new SyncInventory();

    public delegate void OnInventoryChanged();
    public event OnInventoryChanged OnChanged;

    public override void OnStartClient()
    {
        base.OnStartClient();
        inventoryItems.Callback += OnInventoryUpdated;
    }

    private void OnInventoryUpdated(SyncInventory.Operation op, int key, int oldValue, int newValue)
    {
        OnChanged?.Invoke();
    }

    [Command]
    public void CmdAddItem(int itemId, int amount)
    {
        if (inventoryItems.ContainsKey(itemId))
            inventoryItems[itemId] += amount;
        else
            inventoryItems[itemId] = amount;
    }

    [Command]
    public void CmdRemoveItem(int itemId, int amount)
    {
        if (!inventoryItems.ContainsKey(itemId)) return;

        inventoryItems[itemId] -= amount;
        if (inventoryItems[itemId] <= 0)
            inventoryItems.Remove(itemId);
    }

    public int GetItemCount(int itemId)
    {
        return inventoryItems.ContainsKey(itemId) ? inventoryItems[itemId] : 0;
    }

    public Dictionary<int, int> GetInventorySnapshot()
    {
        return new Dictionary<int, int>(inventoryItems);
    }

    public override void OnStopClient()
    {
        inventoryItems.Callback -= OnInventoryUpdated;
    }
}