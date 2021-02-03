using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New AmmoData", menuName = "Data/AmmoData")]
[System.Serializable]
public class AmmoDataStruct : ScriptableObject
{
    public WeaponType weaponType;
    [SerializeField] public int MaxPrimaryAmmo;

    [SerializeField] public int CurrentPrimaryAmmo
    {
        get
        {
            return currentPrimary;
        }
        set
        {
            if (value <0)
            {
                currentPrimary = 0;
            }
            if (value > MaxPrimaryAmmo)
            {
                currentPrimary = MaxPrimaryAmmo;
            }
            else
                currentPrimary = value;
        }
    }

    [SerializeField] private int currentPrimary;
    public AmmoDataStruct(WeaponType weaponType, int maxPrimary, int MaxSecondary, int currentPrimary, int currentSecondary)
    {
        this.weaponType = weaponType;
        MaxPrimaryAmmo = maxPrimary;
        CurrentPrimaryAmmo = currentPrimary;
    }
    public AmmoDataStruct()
    {
    }
}

