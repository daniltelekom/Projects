using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour
{
    public string bulletTag = "ShotgunBullet";
    public Transform firePoint;
    public int pellets = 6;
    public float spreadAngle = 15f;
    public float bulletSpeed = 20f;

    public void Fire()
    {
        for (int i = 0; i < pellets; i++)
        {
            float angle = Random.Range(-spreadAngle, spreadAngle);
            Quaternion rot = Quaternion.Euler(0, angle, 0) * firePoint.rotation;

            GameObject bullet = BulletPool.Instance.SpawnFromPool(bulletTag, firePoint.position, rot);
            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;
        }
    }
}