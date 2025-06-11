using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunController : MonoBehaviour
{
    [Header("Gun Settings")]
    public GameObject bulletPrefab;               // Prefab metka
    public Transform muzzlePoint;                 // Točka sa koje izlazi metak
    public float bulletSpeed = 30f;               // Početna brzina metka

    //[Header("Input")]
    //public InputActionReference shootAction;      // Referenca na akciju “shoot”
    
    [SerializeField]
    private InputActionReference shootAction;

    private void Start()
    {
        shootAction.action.performed += OnShoot;
    }

    private void OnShoot(InputAction.CallbackContext ctx)
    {
        Fire();
    }

    private void OnDestroy()
    {
        shootAction.action.performed -= OnShoot;
    }

    public void Fire()
    {
        if (bulletPrefab == null || muzzlePoint == null)
        {
            Debug.LogError("[GunController] Nedostaje bulletPrefab ili muzzlePoint!");
            return;
        }

        // Instanciraj metak na muzzlePoint-u
        GameObject bullet = Instantiate(bulletPrefab, muzzlePoint.position, muzzlePoint.rotation);
        
        // Ako prefab ima Rigidbody, dodaj mu brzinu
        if (bullet.TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            rb.linearVelocity = muzzlePoint.forward * bulletSpeed;
        }
        else
        {
            Debug.LogWarning("[GunController] Prefab metka nema Rigidbody komponentu – ne mogu postaviti brzinu.");
        }
    }
}
