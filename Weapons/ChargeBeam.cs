using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeBeam : MonoBehaviour
{
    public Transform firePoint;
    public GameObject beamPrefab;
    public float maxChargeTime = 2f;
    public float minDamage = 10f;
    public float maxDamage = 50f;
    public float maxDistance = 50f;
    public LineRenderer lineRenderer;
    public LayerMask hitMask;

    private float chargeTimer;
    private bool isCharging;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            chargeTimer = 0f;
            isCharging = true;
        }

        if (Input.GetButton("Fire1") && isCharging)
        {
            chargeTimer += Time.deltaTime;
            chargeTimer = Mathf.Clamp(chargeTimer, 0f, maxChargeTime);
        }

        if (Input.GetButtonUp("Fire1") && isCharging)
        {
            Fire();
            isCharging = false;
        }
    }

    void Fire()
    {
        float chargeRatio = chargeTimer / maxChargeTime;
        float damage = Mathf.Lerp(minDamage, maxDamage, chargeRatio);

        Vector3 origin = firePoint.position;
        Vector3 direction = firePoint.forward;

        if (Physics.Raycast(origin, direction, out RaycastHit hit, maxDistance, hitMask))
        {
            if (hit.collider.CompareTag("Enemy"))
                hit.collider.GetComponent<EnemyBase>()?.TakeDamage(damage);

            ShowLaser(origin, hit.point);
        }
        else
        {
            ShowLaser(origin, origin + direction * maxDistance);
        }
    }

    void ShowLaser(Vector3 start, Vector3 end)
    {
        StartCoroutine(FlashLaser(start, end));
    }

    System.Collections.IEnumerator FlashLaser(Vector3 start, Vector3 end)
    {
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);
        lineRenderer.enabled = true;
        yield return new WaitForSeconds(0.2f);
        lineRenderer.enabled = false;
    }
}