
using System.Collections.Generic;
using UnityEngine;

public class AmmoCounter : MonoBehaviour
{

    public delegate void UpdateAmmoCountEvent(int value);
    public static event UpdateAmmoCountEvent UpdateAmmoCountEventHandler;

    public List<AmmoDataStruct> ammoData;

    private AmmoDataStruct tempStr;

    private WeaponType CurrentWeaponType;

    public bool CanAdd(WeaponType weaponType) => GetData(weaponType).CurrentPrimaryAmmo < GetData(weaponType).MaxPrimaryAmmo;

    public bool CanDecriase(WeaponType weaponType)
    {
        return GetData(weaponType).CurrentPrimaryAmmo > 0;
    }

    private void Start()
    {
        CurrentWeaponType = WeaponType.Null;
    }

    public void SetWeaponType(WeaponType weaponType)
    {
        CurrentWeaponType = weaponType;
    }

    public void ChangeAmmoCount(WeaponType weaponType, int value, bool Safety = true)
    {
        if (Safety == false)
        {
            GetData(weaponType).CurrentPrimaryAmmo += value;
        }
        else
        {
            if (value < 0)
            {
                if (CanDecriase(weaponType))
                {
                    GetData(weaponType).CurrentPrimaryAmmo -= Mathf.Abs(value);
                }
            }
            else
            {
                if (CanAdd(weaponType))
                {
                    GetData(weaponType).CurrentPrimaryAmmo += Mathf.Abs(value);
                }
            }
        }
        if (CurrentWeaponType == weaponType)
            UpdateAmmoHud(weaponType);
    }

    private AmmoDataStruct GetData(WeaponType weaponType)
    {
        if (tempStr != null && tempStr.weaponType == weaponType)
        {
            return tempStr;
        }
        else
        {
            foreach (AmmoDataStruct data in ammoData)
            {
                if (data.weaponType == weaponType)
                {
                    return data;
                }
            }
            return null;
        }
    }
    public void UpdateAmmoHud(WeaponType weaponType)
    {
        UpdateAmmoCountEventHandler(GetData(weaponType).CurrentPrimaryAmmo);
    }
}
