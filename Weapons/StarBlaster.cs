using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarBlaster : MonoBehaviour
{
    public string projectileTag = "StarProjectile";
    public Transform firePoint;
    public float fireRate = 0.1f;
    public float projectileSpeed = 30f;
    private float lastFireTime;

    public void TryFire()
    {
        if (Time.time >= lastFireTime + fireRate)
        {
            GameObject proj = BulletPool.Instance.SpawnFromPool(projectileTag, firePoint.position, firePoint.rotation);
            proj.GetComponent<Rigidbody>().velocity = firePoint.forward * projectileSpeed;
            lastFireTime = Time.time;
        }
    }
}