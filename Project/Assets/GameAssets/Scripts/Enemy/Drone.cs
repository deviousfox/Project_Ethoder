using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : Enemy
{
    
    public EnemyShooter[] Shooters;
    public float ExplosionForce = 10;

    private Rigidbody GravityRb;
    

    public override void Start()
    {
        GravityRb = GravityRb ?? GetComponent<Rigidbody>();
        foreach (var item in Shooters)
        {
            item.SetDamage(Damage);
        }
        base.Start();
    }

    public override void EnemyDie()
    {
        isDie = true;
        foreach (var item in Shooters)
        {
            if (item != null)
            {
                item.StopAllCoroutines();
            }
        }
        GravityRb.isKinematic = false;
        Instantiate(HitboxDestroyParticle, transform.position, Quaternion.identity);


        foreach (var hitbox in Hitboxes)
        {
            if (hitbox != null)
            {
                Rigidbody rb = hitbox.GetComponent<Rigidbody>();
                hitbox.transform.SetParent(null);
                if (rb != null)
                {
                    rb.isKinematic = false;
                    rb.AddExplosionForce(ExplosionForce, transform.position, 4f);
                }
            }
        }
       

        base.EnemyDie();
        Destroy(gameObject);
    }

    private void Update()
    {
        if (isDie != true)
        {
            transform.LookAt(LevelData.PlayerPosition);
            if (Time.frameCount % 15 == 0)
            {
                if (PlayerIsVisible())
                {
                    foreach (var item in Shooters)
                    {
                        if (item != null)
                        {
                            item.StartCoroutine("Shoot");
                        }
                    }
                }
                else
                {
                    foreach (var item in Shooters)
                    {
                        if (item != null)
                        {
                            item.StopAllCoroutines();
                        }
                    }
                }
            }
        }
       
    }
    public bool PlayerIsVisible()
    {
        Vector3 rayOrigin = transform.position;
        Vector3 rayDirection = LevelData.PlayerPosition - rayOrigin;
        RaycastHit raycastHit = new RaycastHit();
        Physics.Raycast(rayOrigin, rayDirection, out raycastHit);
        
        return raycastHit.collider.gameObject.CompareTag("Player");
        
    }

}
