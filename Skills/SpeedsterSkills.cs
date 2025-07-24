using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedsterSkills : NetworkBehaviour
{
    [Header("Dash")]
    public float dashDistance = 5f;
    public float dashCooldown = 3f;
    public GameObject dashClonePrefab;
    private bool isDashing = false;

    [Header("Attack Buff")]
    public float buffRadius = 5f;
    public float buffDuration = 5f;
    public float fireRateMultiplier = 1.5f;
    public GameObject buffEffect;

    [Header("Lightning Spheres")]
    public GameObject lightningSpherePrefab;
    public float sphereDuration = 5f;
    public float sphereRadius = 3f;
    public float sphereDamageInterval = 1f;
    public float sphereDamage = 10f;

    [Header("Ultimate Vortex")]
    public GameObject vortexEffectPrefab;
    public float vortexDuration = 5f;
    public float vortexRadius = 4f;
    public float vortexDamage = 15f;
    public float vortexDamageInterval = 1f;

    private float lastDashTime;

    void Update()
    {
        if (!isLocalPlayer) return;

        if (Input.GetKeyDown(KeyCode.LeftShift) && Time.time >= lastDashTime + dashCooldown)
        {
            Dash();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ActivateBuff();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ActivateSpheres();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            ActivateVortex();
        }
    }

    #region Dash
    void Dash()
    {
        lastDashTime = Time.time;
        Vector3 dashDirection = transform.forward;
        Vector3 dashTarget = transform.position + dashDirection * dashDistance;

        if (dashClonePrefab)
            Instantiate(dashClonePrefab, transform.position, transform.rotation);

        transform.position = dashTarget;
    }
    #endregion

    #region Attack Buff
    void ActivateBuff()
    {
        Collider[] allies = Physics.OverlapSphere(transform.position, buffRadius);
        foreach (Collider allyCol in allies)
        {
            PlayerStats stats = allyCol.GetComponent<PlayerStats>();
            if (stats != null)
                stats.ApplyFireRateBuff(buffDuration, fireRateMultiplier);
        }

        if (buffEffect)
            Instantiate(buffEffect, transform.position, Quaternion.identity);
    }
    #endregion

    #region Lightning Spheres
    void ActivateSpheres()
    {
        GameObject sphere = Instantiate(lightningSpherePrefab, transform.position, Quaternion.identity, transform);
        StartCoroutine(SphereRoutine(sphere));
    }

    IEnumerator SphereRoutine(GameObject sphere)
    {
        float timer = 0f;

        while (timer < sphereDuration)
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, sphereRadius);
            foreach (Collider col in hits)
            {
                EnemyBase enemy = col.GetComponent<EnemyBase>();
                if (enemy != null)
                {
                    enemy.TakeDamage(sphereDamage);
                }
            }

            yield return new WaitForSeconds(sphereDamageInterval);
            timer += sphereDamageInterval;
        }

        Destroy(sphere);
    }
    #endregion

    #region Ultimate Vortex
    void ActivateVortex()
    {
        StartCoroutine(VortexRoutine());
    }

    IEnumerator VortexRoutine()
    {
        float timer = 0f;
        GameObject effect = Instantiate(vortexEffectPrefab, transform.position, Quaternion.identity);

        while (timer < vortexDuration)
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, vortexRadius);
            foreach (Collider col in hits)
            {
                EnemyBase enemy = col.GetComponent<EnemyBase>();
                if (enemy != null)
                    enemy.TakeDamage(vortexDamage);
            }

            yield return new WaitForSeconds(vortexDamageInterval);
            timer += vortexDamageInterval;
        }

        Destroy(effect);
    }
    #endregion
}
