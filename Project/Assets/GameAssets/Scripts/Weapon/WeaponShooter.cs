using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShooter : MonoBehaviour
{
    public WeaponData Data;
    public Transform BarrelPoint;

    private AudioSource shootSource;
    private Camera mainCam;
    private AmmoCounter ammoCounter;

    private void Start()
    {
        shootSource = GetComponent<AudioSource>();
        mainCam = Camera.main;
        ammoCounter = FindObjectOfType<AmmoCounter>();
        ammoCounter.ChangeAmmoCount(Data.WeaponType, 0, false);
        OnEnable();
    }
    private void OnEnable()
    {
        if (ammoCounter != null)
        {
            ammoCounter.SetWeaponType(Data.WeaponType);
            ammoCounter.UpdateAmmoHud(Data.WeaponType);
        }
    }
    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Mouse0))
    //    {
    //        ShootEvent(ShootingType.Primary);
    //    }
    //}


    public void ShootEvent(ShootingType shootingType)
    {
        if (shootingType == ShootingType.Primary)
        {
            if (!Data.PrimaryObjectShooting)
            {
                RayCastShooting(Data.PrimaryBulletCount, Data.PrimaryShootCost, Data.PrimaryDamage, Data.PrimaryDispersion, Data.RayCastDecal, Data.PrimaryShootSound);
            }
            else
            {
                ObjectShooting(Data.PrimaryBulletCount, Data.PrimaryShootCost, Data.PrimaryDamage, Data.PrimaryBulletSpeed, Data.PrimaryDispersion, Data.PrimaryBulletPrefab, Data.PrimaryShootSound);
            }
            if (Data.PrimaryShootSound != null)
            {
                shootSource.pitch = Random.Range(0.7f, 1.3f);
                shootSource.PlayOneShot(Data.PrimaryShootSound);
            }
        }
        else
        {
            if (!Data.SecondaryObjectShooting)
            {
                RayCastShooting(Data.SecondaryBulletCount, Data.SecondaryShootCost, Data.SecondaryDamage, Data.SecondaryDispersion, Data.RayCastDecal, Data.SecondaryShootSound);
            }
            else
            {
                ObjectShooting(Data.SecondaryBulletCount, Data.SecondaryShootCost, Data.SecondaryDamage, Data.SecondaryBulletSpeed, Data.SecondaryDispersion, Data.SecondaryBulletPrefab, Data.SecondaryShootSound);
            }
            shootSource.pitch = Random.Range(0.7f, 1.3f);
            if (Data.SecondaryShootSound != null)
            {
                shootSource.PlayOneShot(Data.SecondaryShootSound);
            }
        }
    }

    private void RayCastShooting(int bulletCount, int shootCost, float damage, Vector2 dispersion, GameObject decal, AudioClip shootClip)
    {
        for (int i = 0; i < bulletCount; i++)
        {
            Vector3 rayOrigin = mainCam.ViewportToWorldPoint(new Vector2(0.5f, 0.5f));
            Vector3 rayDirection = GetDirection(dispersion);
            RaycastHit shootHit = new RaycastHit();
            if (Physics.Raycast(rayOrigin, rayDirection, out shootHit, 100f, ~(1<<8)))
            {
               HitboxComponent tempHitbox = shootHit.collider.GetComponent<HitboxComponent>();
                if (tempHitbox != null)
                {
                    tempHitbox.ApllyDamage(damage);
                }
                else
                {
                    Instantiate(decal, shootHit.point + shootHit.normal * 0.001f,  Quaternion.FromToRotation(Vector3.forward, shootHit.normal));
                }
            }

        }
        ammoCounter.ChangeAmmoCount(Data.WeaponType, -shootCost);
    }

    private void ObjectShooting(int bulletCount, int shootCost, float damage, float bulletSpeed, Vector2 dispersion, GameObject bulletPref, AudioClip shootClip)
    {
        for (int i = 0; i < bulletCount; i++)
        {
            Vector3 rayOrigin = mainCam.ViewportToWorldPoint(new Vector2(0.5f, 0.5f));
            Vector3 rayDirection = GetDirection(dispersion);
            RaycastHit shootHit = new RaycastHit();
            if (Physics.Raycast(rayOrigin,rayDirection, out shootHit,100f, ~(1 <<8)))
            {
                GameObject bullet = Instantiate(bulletPref, BarrelPoint.position, transform.rotation) as GameObject;
                bullet.GetComponent<Rigidbody>().AddForce(GetShootDirection(BarrelPoint.position, shootHit.point) * bulletSpeed, ForceMode.Impulse);
                bullet.GetComponent<Bullet>().damage = GetRndDamage(damage);
            }
        }
        ammoCounter.ChangeAmmoCount(Data.WeaponType, -shootCost);
      //  Debug.Break();
    }


    private float GetRndDamage(float damage)
    {
        return Mathf.Abs(Random.Range(damage*0.7f,damage*1.3f));
    }
    private Vector3 GetDirection(Vector2 dispersion)
    {
        return new Vector3(Random.Range(-dispersion.x, dispersion.x), Random.Range(-dispersion.y, dispersion.y), Random.Range(-dispersion.x, dispersion.x)) + (mainCam.transform.forward * 100f);
    }
    private Vector3 GetShootDirection(Vector3 startPosition, Vector3 endPosition)
    {
        return (endPosition - startPosition).normalized;
    }
}

