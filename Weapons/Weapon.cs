using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponData data;
    public Transform firePoint;
    private float lastFireTime;
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
                var bullet = BulletPool.Instance.SpawnFromPool("DefaultBullet", firePoint.position, firePoint.rotation);
                bullet.GetComponent<Rigidbody>().velocity = firePoint.forward * data.projectileSpeed;
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