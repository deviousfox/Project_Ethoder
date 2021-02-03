using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponChanger : MonoBehaviour
{
    public GameObject[] Weapons = new GameObject[9];

    public int currentWeapon;

    public int wishWeapon;

    private int lastWeapon = 0;


    public delegate void UpdateWeaponImageEvent(Sprite weaponImage, Sprite weaponTarget);
    public static event UpdateWeaponImageEvent UpdateWeaponImageEventHandler;


    private void Start()
    {
        foreach (GameObject weapon in Weapons)
        {
            if (weapon != null)
            {
                weapon.SetActive(false);
            }
        }
        if (Weapons[lastWeapon] != null)
        {
            Weapons[lastWeapon].SetActive(true);
            UpdateWeaponImageEventHandler(Weapons[lastWeapon].GetComponent<WeaponShooter>().Data.WeaponImage,
                Weapons[lastWeapon].GetComponent<WeaponShooter>().Data.WeaponTarget);
        }
        else
        {
            UpdateWeaponImageEventHandler(null, null);
        }
    }

    private void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0.05)
        {
            wishWeapon++;
            if (wishWeapon > Weapons.Length - 1)
            {
                wishWeapon = 0;
            }
            //ChangeWeapon(wishWeapon);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < -0.05)
        {
            wishWeapon--;
            if (wishWeapon < 0)
            {
                wishWeapon = Weapons.Length - 1;
            }
            //ChangeWeapon(wishWeapon);
        }
        if (wishWeapon != currentWeapon && Weapons[wishWeapon] != null)
        {
            if (Weapons[currentWeapon].GetComponent<WeaponAnimationController>() != null)
            {
                Weapons[currentWeapon].GetComponent<WeaponAnimationController>().Hide();
            }
            Invoke("ChangeWeapon", 1f);
        }
    }
	private void ChangeWeapon()
	{
		ChangeWeapon(wishWeapon);
	}

    public void ChangeWeapon(int index)
    {
        
        if (Weapons[index] != null)
        {
            foreach (GameObject weapon in Weapons)
            {
                if (weapon != null)
                {
                    weapon.SetActive(false);
                }
            }
            Weapons[index].SetActive(true);
            currentWeapon = index;
        }
        else
        {
            if (currentWeapon < index)
            {
                for (int i = index; i < Weapons.Length; i++)
                {
                    if (Weapons[i] !=null)
                    {
                        foreach (GameObject weapon in Weapons)
                        {
                            if (weapon != null)
                            {
                                weapon.SetActive(false);
                            }
                        }
                        Weapons[i].SetActive(true);
                        currentWeapon = i;
                        wishWeapon = i;
                        break;
                    }
                }
            }
            else
            {
                for (int i = index; i >-1 ; i--)
                {
                    if (Weapons[i] != null)
                    {
                        foreach (GameObject weapon in Weapons)
                        {
                            if (weapon != null)
                            {
                                weapon.SetActive(false);
                            }
                        }
                        Weapons[i].SetActive(true);
                        currentWeapon = i;
                        wishWeapon = i;
                        break;
                    }
                }
            }
        }
        UpdateWeaponImageEventHandler(Weapons[currentWeapon].GetComponent<WeaponShooter>().Data.WeaponImage,
            Weapons[currentWeapon].GetComponent<WeaponShooter>().Data.WeaponTarget);
    }

    public void AddWeapon(int index, GameObject Weapon)
    {
        if (Weapons[index] == null)
        {
            Weapons[index] = Instantiate(Weapon, this.transform) as GameObject;
            foreach (GameObject weapon in Weapons)
            {
                if (weapon != null)
                {
                    weapon.SetActive(false);
                }
            }
            Weapons[index].SetActive(true);
            UpdateWeaponImageEventHandler(Weapons[index].GetComponent<WeaponShooter>().Data.WeaponImage,
                Weapons[index].GetComponent<WeaponShooter>().Data.WeaponTarget);
        } 
    }
}
