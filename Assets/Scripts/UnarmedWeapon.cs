using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnarmedWeapon : Weapon
{
    public override bool Fire()
    {
        //1
        if (!base.Fire())
        {
            return false;
        }
        //5 = doStuff

        return true;
    }
}