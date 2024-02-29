using UnityEngine.InputSystem;
using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{
    public Transform fpsCam;
    public float range;
    public float impactForce;
    public int damageAmount;
    public int fireRate;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public int currentAmmo;
    public int maxAmmo;
    public int magazineAmmo;
    public float reloadTime;
    public bool isReloading;

    protected InputAction shoot;

    protected float nextTimeToFire = 0;

    protected virtual void Start()
    {
        shoot = new InputAction("Shoot", binding: "<mouse>/leftButton");
        shoot.AddBinding("<Gamepad>/x");

        shoot.performed += _ => Shoot();
        shoot.Enable();
    }

    protected virtual void Update()
    {
        // To be implemented in child classes
    }

    protected virtual void Shoot()
    {
        // To be implemented in child classes
    }

    protected virtual IEnumerator Reload()
    {
        // To be implemented in child classes
        yield return null;
    }
}