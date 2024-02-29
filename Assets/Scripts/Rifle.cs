using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : Gun
{
    protected override void Start()
    {
        base.Start();

        // Customize attributes for Rifle
        range = 20f;
        impactForce = 150f;
        damageAmount = 15;
        fireRate = 12;
        maxAmmo = 20;
        magazineAmmo = 160;
        reloadTime = 2.0f;
    }
}