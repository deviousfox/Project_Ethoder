using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : Puckup
{
    public int WeaponIndex;
    public int AdditionalAmmo;
    public WeaponType WeaponType;
    public GameObject PlayerWeaponInstance;
    private WeaponChanger weaponChanger;
    private AmmoCounter ammoCounter;

    private void Awake()
    {
        
    }

    public override void Inititialize()
    {
        ammoCounter = WorldState.AmmoCounter;
        weaponChanger = WorldState.WeaponChanger;
    }
    public override void Pickup()
    {
        weaponChanger.AddWeapon(WeaponIndex, PlayerWeaponInstance);
        ammoCounter.ChangeAmmoCount(WeaponType, AdditionalAmmo);
        Destroy(gameObject);
    }
}
