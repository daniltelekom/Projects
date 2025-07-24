using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SniperSkills : NetworkBehaviour
{
    public GameObject piercingBulletPrefab;
    public Transform firePoint;

    public float piercingDamage = 80f;
    public float piercingCooldown = 10f;
    private float lastPiercingTime;

    public float critBuffDuration = 5f;
    public float critMultiplierBonus = 1.5f;
    public float critCooldown = 15f;
    private float lastCritBuffTime;

    public float trapRadius = 3f;
    public float trapSlowAmount = 0.5f;
    public float trapDuration = 4f;
    public float trapCooldown = 12f;
    private float lastTrapTime;

    public GameObject ultimateShotPrefab;
    public float ultimateDamage = 250f;
    public float ultimateCooldown = 30f;
    private float lastUltimateTime;

    private PlayerStats stats;

    private void Start()
    {
        stats = GetComponent<PlayerStats>();
    }

    private void Update()
    {
        if (!isLocalPlayer) return;

        // Примеры вызовов скиллов (в бою заменишь на кнопку)
        if (Input.GetKeyDown(KeyCode.Alpha1)) CmdUsePiercingShot();
        if (Input.GetKeyDown(KeyCode.Alpha2)) CmdActivateCritBuff();
        if (Input.GetKeyDown(KeyCode.Alpha3)) CmdPlaceTrap();
        if (Input.GetKeyDown(KeyCode.Alpha4)) CmdUltimateShot();
    }

    // --- АКТИВКА 1: ПУЛЯ НАСКВОЗЬ ---
    [Command]
    void CmdUsePiercingShot()
    {
        if (Time.time < lastPiercingTime + piercingCooldown) return;
        lastPiercingTime = Time.time;

        GameObject bullet = Instantiate(piercingBulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<ProjectilePiercing>().Initialize(piercingDamage, transform.forward);
        NetworkServer.Spawn(bullet);
    }

    // --- АКТИВКА 2: КРИТИЧЕСКИЙ БАФ ---
    [Command]
    void CmdActivateCritBuff()
    {
        if (Time.time < lastCritBuffTime + critCooldown) return;
        lastCritBuffTime = Time.time;

        StartCoroutine(ApplyCritBuff());
    }

    IEnumerator ApplyCritBuff()
    {
        float originalMultiplier = stats.critMultiplier;
        stats.critMultiplier += critMultiplierBonus;
        stats.onStatsUpdated?.Invoke();
        yield return new WaitForSeconds(critBuffDuration);
        stats.critMultiplier = originalMultiplier;
        stats.onStatsUpdated?.Invoke();
    }

    // --- АКТИВКА 3: ЛОВУШКА ---
    [Command]
    void CmdPlaceTrap()
    {
        if (Time.time < lastTrapTime + trapCooldown) return;
        lastTrapTime = Time.time;

        Collider[] enemies = Physics.OverlapSphere(transform.position, trapRadius);
        foreach (var hit in enemies)
        {
            if (hit.CompareTag("Enemy"))
            {
                EnemyBase enemy = hit.GetComponent<EnemyBase>();
                if (enemy != null)
                {
                    enemy.ApplyStatus(StatusEffectType.Slow, trapDuration, trapSlowAmount);
                }
            }
        }
    }

    // --- УЛЬТА: СТРЕЛА БОГА ---
    [Command]
    void CmdUltimateShot()
    {
        if (Time.time < lastUltimateTime + ultimateCooldown) return;
        lastUltimateTime = Time.time;

        GameObject bullet = Instantiate(ultimateShotPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<ProjectilePiercing>().Initialize(ultimateDamage, transform.forward, true);
        NetworkServer.Spawn(bullet);
    }
}