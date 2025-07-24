using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : NetworkBehaviour
{
    public float moveSpeed = 5f;
    public float reloadTime = 1.5f;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public Camera cam;

    private Rigidbody rb;
    private Vector3 moveInput;
    private Vector3 moveVelocity;

    private bool isReloading = false;
    private bool isDead = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (!isLocalPlayer)
        {
            if (cam != null)
                cam.gameObject.SetActive(false);
            return;
        }

        if (cam == null)
            cam = Camera.main;

        PlayerStats stats = GetComponent<PlayerStats>();
        if (stats != null)
        {
            stats.OnPlayerDied += () =>
            {
                isDead = true;
                rb.velocity = Vector3.zero;
            };
        }
    }

    void Update()
    {
        if (!isLocalPlayer || isDead) return;

        moveInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        moveVelocity = moveInput.normalized * moveSpeed;

        RotateToMouse();

        if (Input.GetButtonDown("Fire1") && !isReloading)
        {
            CmdShoot();
            StartCoroutine(ReloadCoroutine());
        }
    }

    void FixedUpdate()
    {
        if (!isLocalPlayer ||  isDead || rb == null) return;

        rb.velocity = moveVelocity;
    }

    void RotateToMouse()
    {
        if (cam == null) return;

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, LayerMask.GetMask("Default")))
        {
            Vector3 lookPoint = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            transform.LookAt(lookPoint);
        }
    }

    [Command]
    void CmdShoot()
    {
        if (bulletPrefab == null || firePoint == null) return;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        NetworkServer.Spawn(bullet);
    }

    IEnumerator ReloadCoroutine()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        isReloading = false;
    }
}