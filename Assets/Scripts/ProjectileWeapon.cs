using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileWeapon : Weapon
{
    public Projectile ProjectileToSpawn = null;

    public override bool Fire()
    {
        if (!base.Fire())
        {
            return false;
        }

        Projectile spawnedProjectile = Instantiate(ProjectileToSpawn);
        spawnedProjectile.Init(transform.position,
            Camera.main.transform.forward.normalized * 10000f + Camera.main.transform.position);

        return true;
    }
}