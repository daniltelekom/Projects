using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponData data;
    public Transform firePoint;
    private float lastFireTime;

case WeaponType.ChargeBeam:
    GetComponent<ChargeBeam>().enabled = true;
    break;
case WeaponType.EnergyOrb:
    var orb = BulletPool.Instance.SpawnFromPool("EnergyOrb", firePoint.position, firePoint.rotation);
    orb.GetComponent<Rigidbody>().velocity = firePoint.forward * data.projectileSpeed;
    break;

    private void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= lastFireTime + data.fireDelay)
        {
            Fire();
            lastFireTime = Time.time;
        }
    }

    void Fire()
    {
        switch (data.weaponType)
        {
            case WeaponType.Default:
                Instantiate(data.projectilePrefab, firePoint.position, firePoint.rotation)
                    .GetComponent<Rigidbody>().velocity = firePoint.forward * data.projectileSpeed;
                break;
            case WeaponType.Shotgun:
                GetComponent<Shotgun>().Fire();
                break;
            case WeaponType.Laser:
                data.laserPrefab.SetActive(true);
                break;
            case WeaponType.StarBlaster:
                GetComponent<StarBlaster>().TryFire();
                break;
        }
    }
}