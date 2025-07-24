using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public LineRenderer line;
    public ParticleSystem laserFlash;
    public float duration = 0.2f;
    public float damage = 5f;
    public LayerMask hitMask;

    private void OnEnable()
    {
        FireLaser();
        Invoke(nameof(Disable), duration);
    }

    void FireLaser()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        Vector3 endPoint = transform.position + transform.forward * 100f;

        if (Physics.Raycast(ray, out RaycastHit hit, 100f, hitMask))
        {
            endPoint = hit.point;

            if (hit.collider.CompareTag("Enemy"))
                hit.collider.GetComponent<EnemyBase>()?.TakeDamage(damage);
        }

        line.enabled = true;
        line.positionCount = 2;
        line.SetPosition(0, transform.position);
        line.SetPosition(1, endPoint);

        if (laserFlash != null)
        {
            laserFlash.transform.position = transform.position;
            laserFlash.Play();
        }
    }

    void Disable()
    {
        line.enabled = false;
        gameObject.SetActive(false);
    }
}
