using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponState
{
    Unarmed = 0,
    HitScan = 1,
    Projectile = 2,
    Total
}

public class WeaponBehaviour : MonoBehaviour
{
    /*[Header("Unarmed = Element 0 \n" + 
        "Hitscan = Element 1 \n" + 
        "Projectile = Element 2")]*/
    public Weapon[] AvailableWeapons = new Weapon[(int)WeaponState.Total];
    public Weapon currentWeapon = null;
    public float scrollWheelBreakpoint = 1f;
    private float scrollWheelDelta = 0f;

    private void Update()
    {
        HandleWeaponSwap();
    }

    private void HandleWeaponSwap()
    {
        scrollWheelDelta += Input.mouseScrollDelta.y;
        if (Mathf.Abs(scrollWheelDelta) > scrollWheelBreakpoint)
        {
            int swapDirection = (int)Mathf.Sign(scrollWheelDelta);
            scrollWheelDelta -= swapDirection * scrollWheelBreakpoint;

            int currentWeaponIndex = (int)currentWeapon.WeaponType;
            currentWeaponIndex += swapDirection;

            if (currentWeaponIndex < 0)
            {
                currentWeaponIndex = (int)WeaponState.Total + currentWeaponIndex;
            }
            if (currentWeaponIndex >= 0)
            {
                currentWeaponIndex = 0;
            }
            currentWeapon = AvailableWeapons[currentWeaponIndex];
        }
    }
}