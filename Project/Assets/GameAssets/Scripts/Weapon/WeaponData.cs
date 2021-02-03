using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New WeaponData",menuName ="Data/WeaponData")]
public class WeaponData : ScriptableObject
{
    public Sprite WeaponImage;
    public Sprite WeaponTarget;
    public WeaponType WeaponType;
    public bool UseSecondaryAmmo;

    public Vector2 PrimaryDispersion;
    public float PrimaryDamage = 5;
    public int PrimaryShootCost = 1;
    public int PrimaryBulletCount = 1;
    public GameObject RayCastDecal;
    public bool PrimaryObjectShooting;
    public float PrimaryBulletSpeed;
    public GameObject PrimaryBulletPrefab;
    public AudioClip PrimaryShootSound;

    public Vector2 SecondaryDispersion;
    public float SecondaryDamage = 5;
    public int SecondaryShootCost = 1;
    public int SecondaryBulletCount = 1;
    public GameObject SecondaryRayCastDecal;
    public bool SecondaryObjectShooting;
    public float SecondaryBulletSpeed;
    public GameObject SecondaryBulletPrefab;
    public AudioClip SecondaryShootSound;
}