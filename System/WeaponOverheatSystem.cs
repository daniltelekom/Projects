using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponOverheatSystem : MonoBehaviour
{
    public float heat;
    public float maxHeat = 100f;
    public float heatPerShot = 20f;
    public float coolRate = 10f;
    public bool isOverheated => heat >= maxHeat;

    public void Fire()
    {
        if (isOverheated) return;

        heat += heatPerShot;
        heat = Mathf.Min(heat, maxHeat);
    }

    void Update()
    {
        if (heat > 0)
            heat -= coolRate * Time.deltaTime;
    }

    public void ResetHeat() => heat = 0;
}