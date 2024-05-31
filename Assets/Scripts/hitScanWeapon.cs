using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitScanWeapon : Weapon
{
    [SerializeField] ParticleSystem hitParticle;

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

        Ray weaponRay = new Ray(mainCam.transform.position, mainCam.transform.forward);
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(weaponRay, out hit, weaponRange, ~hitMask))
        {
            hitParticle.transform.position = hit.point;
            hitParticle.gameObject.SetActive(true);
            hitParticle.Play();
        }

        return true;
    }

    public void HitScanFire(int gunNumber)
    {
        MouseLook player = FindObjectOfType<MouseLook>();
        WeaponBehaviour handler = FindObjectOfType<WeaponBehaviour>();

        RaycastHit hit;

        handler.GetSelectedGun()[gunNumber].fireEffect.Play();
        handler.GetSelectedGun()[gunNumber].currentAmmo--;

        if (Physics.Raycast(handler.GetFirePoint(), player.GetAimPoint(), out hit, handler.GetSelectedGun()[gunNumber].range))
        {
            Enemy enemy = hit.transform.GetComponent<Enemy>();
            GameObject impact = Instantiate(handler.GetSelectedGun()[gunNumber].impactEffect, hit.point, Quaternion.LookRotation(hit.normal));

            if (enemy != null)
            {
                enemy.TakeDamage(handler.GetSelectedGun()[gunNumber].damage);
            }

            impact.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
            Destroy(impact, 1f);
        }
    }
}