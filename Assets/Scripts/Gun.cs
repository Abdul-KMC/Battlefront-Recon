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
    protected float nextTimeToFire = 1.0f;
    public bool isReloading;

    InputAction shoot;

    protected virtual void Start()
    {
        shoot = new InputAction("Shoot", binding: "<mouse>/leftButton");
        shoot.AddBinding("<Gamepad>/x");

        shoot.performed += _ => Fire();  // Attach Fire() method to the performed event
        shoot.Enable();

        currentAmmo = maxAmmo;
    }

    private void OnEnable()
    {
        isReloading = false;
    }

    protected virtual void Update()
    {
        if (currentAmmo == 0 && magazineAmmo == 0)
        {
            return;
        }

        if (isReloading)
            return;

        bool isShooting = shoot.ReadValue<float>() > 0.5f;

        if (isShooting && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Fire();
        }

        if (currentAmmo == 0 && magazineAmmo > 0 && !isReloading)
        {
            StartCoroutine(Reload());
        }
    }

    private void Fire()
    {
        AudioManager.instance.Play("Shoot");

        muzzleFlash.Play();
        currentAmmo--;
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.position + fpsCam.forward, fpsCam.forward, out hit, range))
        {
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            Enemy e = hit.transform.GetComponent<Enemy>();
            if (e != null)
            {
                e.TakeDamage(damageAmount);
                return;
            }

            Quaternion impactRotation = Quaternion.LookRotation(hit.normal);
            GameObject impact = Instantiate(impactEffect, hit.point, impactRotation);
            impact.transform.parent = hit.transform;
            Destroy(impact, 5);
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        // AudioManager.instance.Play("Reload");
        yield return new WaitForSeconds(reloadTime);
        if (magazineAmmo >= maxAmmo)
        {
            currentAmmo = maxAmmo;
            magazineAmmo -= maxAmmo;
        }
        else
        {
            currentAmmo = magazineAmmo;
            magazineAmmo = 0;
        }
        isReloading = false;
    }
}
