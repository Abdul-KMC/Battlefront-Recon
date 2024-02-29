using UnityEngine.InputSystem;
using UnityEngine;
using System.Collections;

public class Pistol : Gun
{
    protected override void Start()
    {
        base.Start();
        range = 10f;
        impactForce = 100f;
        damageAmount = 10;
        fireRate = 8;
        maxAmmo = 8;
        magazineAmmo = 96;
        reloadTime = 1.5f;
        currentAmmo = maxAmmo;
    }
    protected override void Update()
    {
        if (shoot.triggered && currentAmmo > 0)
        {
            Shoot();
        }

        if (currentAmmo == 0 && magazineAmmo > 0 && !isReloading)
        {
            StartCoroutine(Reload());
        }
    }
    protected override void Shoot()
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

    protected override IEnumerator Reload()
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