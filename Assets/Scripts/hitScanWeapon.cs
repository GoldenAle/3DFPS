using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HitScanWeapon : Weapon
{
    public ParticleSystem hitParticle;

    private new void Start()
    {
        hitParticle.gameObject.SetActive(false);
        base.Start();
    }

    public override bool Fire()
    {
        if (!base.Fire())
        {
            return false;
        }

        return base.Fire();

        Ray weaponRay = new Ray(mainCam.transform.position, mainCam.transform.forward);
        RaycastHit hit = new();
        if (Physics.Raycast(weaponRay, out hit, weaponRange, ~hitMask))
        {
            hitParticle.transform.position = hit.point;
            hitParticle.gameObject.SetActive(true);
            hitParticle.Play();
        }
    }
}