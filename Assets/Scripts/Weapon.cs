using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] int ammunition = 0;
    public LayerMask hitMask = 0;
    public float weaponRange = 100.0f;
    public WeaponState WeaponType = WeaponState.Total;

    protected Camera mainCam = null;

    protected void Start()
    {
        mainCam = Camera.main;
    }

    public virtual bool Fire()
    {
        if (ammunition < 1)
        {
            return false;
        }
        ammunition--;
        return true;
    }
}