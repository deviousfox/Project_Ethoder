
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : Puckup
{
    private AmmoCounter ammoCounter;

    public WeaponType WeaponType;
    public int AmmoCount;

    
    public override void Inititialize()
    {
        ammoCounter = WorldState.AmmoCounter;
    }
    public override void Pickup()
    {
        if (ammoCounter.CanAdd(WeaponType))
        {
            ammoCounter.ChangeAmmoCount(WeaponType, AmmoCount, false);
            PlaySound();
            Destroy(gameObject);
        }
    }
}
