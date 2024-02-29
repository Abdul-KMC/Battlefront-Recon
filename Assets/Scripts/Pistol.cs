using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Gun
{
    protected override void Start()
    {
        base.Start();

        // Customize attributes for Pistol
        range = 10f;
        impactForce = 100f;
        damageAmount = 10;
        fireRate = 8;
        maxAmmo = 8;
        magazineAmmo = 100;
        reloadTime = 1.5f;
    }
}
