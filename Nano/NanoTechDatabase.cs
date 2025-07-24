using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NanoTechDatabase", menuName = "Nano/NanoTech Database")]
public class NanoTechDatabase : ScriptableObject
{
    public static NanoTechDatabase Instance { get; private set; }

    [SerializeField]
    private List<StatusEffectData> allEffects;

    private Dictionary<string, StatusEffectData> idToEffectMap;

    private void OnEnable()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning("Duplicate NanoTechDatabase in project!");
            return;
        }

        Instance = this;
        InitializeMap();
    }

    private void InitializeMap()
    {
        idToEffectMap = new Dictionary<string, StatusEffectData>();
        foreach (var effect in allEffects)
        {
            if (!string.IsNullOrEmpty(effect.id))
            {
                if (!idToEffectMap.ContainsKey(effect.id))
                    idToEffectMap.Add(effect.id, effect);
                else
                    Debug.LogWarning($"Duplicate effect ID found: {effect.id}");
            }
        }
    }

    public StatusEffectData GetEffectById(string id)
    {
        if (string.IsNullOrEmpty(id)) return null;
        return idToEffectMap != null && idToEffectMap.TryGetValue(id, out var effect) ? effect : null;
    }
}
