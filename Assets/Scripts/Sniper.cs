using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : Gun
{
    protected override void Start()
    {
        base.Start();

        // Customize attributes for Sniper
        range = 30f;
        impactForce = 200f;
        damageAmount = 20;
        fireRate = 2;
        maxAmmo = 5;
        magazineAmmo = 100;
        reloadTime = 3.0f;
    }
}