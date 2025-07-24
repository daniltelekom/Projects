using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapons/WeaponData")]
public class WeaponData : ScriptableObject
{
    public string weaponName;
    public WeaponType weaponType;

    public GameObject projectilePrefab;
    public GameObject laserPrefab;

    public float fireDelay = 0.5f;
    public float projectileSpeed = 20f;
    public float damage = 10f;
}

public enum WeaponType
{
    Default,
    Shotgun,
    Laser,
    StarBlaster,
    ChargeBeam,
    EnergyOrb
}