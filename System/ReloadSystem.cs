using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadSystem : MonoBehaviour
{
    public int currentAmmo;
    public int maxAmmo = 6;
    public float reloadTime = 2f;
    public bool isReloading = false;

    public System.Action OnReload;

    public void Fire()
    {
        if (isReloading || currentAmmo <= 0) return;
        currentAmmo--;
    }

    public void StartReload()
    {
        if (isReloading || currentAmmo == maxAmmo) return;
        StartCoroutine(ReloadCoroutine());
    }

    private System.Collections.IEnumerator ReloadCoroutine()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        isReloading = false;
        OnReload?.Invoke();
    }
}