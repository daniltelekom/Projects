using UnityEngine;

[CreateAssetMenu(fileName = "NewStatusEffect", menuName = "Nano/StatusEffect")]
public class StatusEffectData : ScriptableObject
{
    public string id; // Новый параметр для связи с NanoTechnology
    public string effectName;
    public StatusEffectType effectType;
    public float duration;
    public float tickInterval;
    public float value;
    public GameObject effectVFX;
}