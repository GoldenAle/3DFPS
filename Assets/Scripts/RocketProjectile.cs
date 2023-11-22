using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketProjectile : Projectile
{
    protected override void Update()
    {
        base.Update();
        transform.position += movementSpeed * Time.deltaTime * aimDirection;
    }
}