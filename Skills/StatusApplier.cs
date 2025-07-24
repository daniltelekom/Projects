using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusApplier : MonoBehaviour
{
    public StatusEffectData effect;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            var manager = other.GetComponent<StatusEffectManager>();
            if (manager != null)
            {
                manager.ApplyEffect(new StatusEffectInstance(effect));
            }
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            var manager = other.GetComponent<StatusEffectManager>();
            if (manager != null)
            {
                manager.ApplyEffect(new StatusEffectInstance(effect));
            }
        }
    }
}