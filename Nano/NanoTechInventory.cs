using UnityEngine;
using Mirror;
using System.Collections.Generic;

public class NanoTechInventory : NetworkBehaviour
{
    [SyncVar] public int maxSlots = 4;

    [SyncVar(hook = nameof(OnTechUpdated))]
    public List<string> equippedTechIds = new(); // ID эффектов

    public delegate void NanoTechChanged();
    public event NanoTechChanged OnChanged;

    public void Equip(NanoTechnology tech)
    {
        if (equippedTechIds.Count >= maxSlots) return;
        equippedTechIds.Add(tech.effectId);
        ApplyTech(tech.effectId);
        OnChanged?.Invoke();
    }

    public void ApplyTech(string effectId)
    {
        var data = NanoTechDatabase.Instance.GetEffectById(effectId);
        if (data != null && TryGetComponent(out NanoEffectSystem effectSystem))
        {
            effectSystem.ApplyEffect(data);
        }
    }

    void OnTechUpdated(List<string> oldVal, List<string> newVal)
    {
        OnChanged?.Invoke();
    }
}