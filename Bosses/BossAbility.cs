using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAbility : MonoBehaviour
{
    public string abilityName;
    public GameObject effectPrefab;
    public float duration = 3f;

    public void Activate()
    {
        Debug.Log($"Активирована способность босса: {abilityName}");

        if (effectPrefab != null)
        {
            Instantiate(effectPrefab, transform.position, Quaternion.identity);
        }

        // Тут можно расширить: вызвать волну, телепорт, ульту и т.д.
    }
}