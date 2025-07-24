using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafriSkills : MonoBehaviour
{
    public GameObject fireballPrefab;
    public GameObject flameCirclePrefab;
    public GameObject fireShieldPrefab;
    public GameObject ultimateFlameRingPrefab;
    public Transform firePoint;

    private PlayerStats stats;
    private SkillTreeManager skillTree;
    private GameObject activeShield;

    void Start()
    {
        stats = GetComponent<PlayerStats>();
        skillTree = GetComponent<SkillTreeManager>();
    }

    // 1. ОГНЕННЫЙ ШАР
    public void UseFireball()
    {
        GameObject fb = Instantiate(fireballPrefab, firePoint.position, firePoint.rotation);
        Fireball fireball = fb.GetComponent<Fireball>();
        fireball.damage = 25f + skillTree.GetSkillBonus("FireballDamage");
        fireball.lifeTime = 4f + skillTree.GetSkillBonus("FireballDuration");

        // задаём статус-эффект огня
        fireball.statusEffect = new StatusEffectData(StatusEffectType.Burn, 5f, 5f);
    }

    // 2. ПЛАМЕННЫЙ КРУГ
    public void UseFlameCircle()
    {
        GameObject circle = Instantiate(flameCirclePrefab, transform.position, Quaternion.identity);
        FlameCircle fc = circle.GetComponent<FlameCircle>();
        fc.damagePerSecond = 10f + skillTree.GetSkillBonus("FlameCircleDPS");
        fc.duration = 4f + skillTree.GetSkillBonus("FlameCircleDuration");
        fc.statusEffect = new StatusEffectData(StatusEffectType.Burn, 6f, 4f);
    }

    // 3. ОГНЕННЫЙ ЩИТ
    public void ActivateFireShield()
    {
        if (activeShield != null) Destroy(activeShield);

        activeShield = Instantiate(fireShieldPrefab, transform);
        FireShield fs = activeShield.GetComponent<FireShield>();
        fs.damage = 5f + skillTree.GetSkillBonus("ShieldDamage");
        fs.tickRate = 0.5f;
        fs.statusEffect = new StatusEffectData(StatusEffectType.Burn, 4f, 3f);

        float duration = 6f + skillTree.GetSkillBonus("ShieldDuration");
        Destroy(activeShield, duration);
    }

    // 4. УЛЬТИМЕЙТ — ОГНЕННОЕ КОЛЬЦО
    public void UseUltimate()
    {
        GameObject ring = Instantiate(ultimateFlameRingPrefab, transform.position, Quaternion.identity);
        UltimateFlameRing ufr = ring.GetComponent<UltimateFlameRing>();
        ufr.damage = 50f + skillTree.GetSkillBonus("UltimateDamage");
        ufr.radius = 5f + skillTree.GetSkillBonus("UltimateRadius");
        ufr.expansionSpeed = 8f;
        ufr.lifeTime = 1.5f;
        ufr.statusEffect = new StatusEffectData(StatusEffectType.Burn, 7f, 6f);
    }
}
