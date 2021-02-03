using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JackCanonBullet : Bullet
{
    public float explosionRadius;
    public float shellSpeed;
    public GameObject shellPrefab;
    public GameObject ExplosionPrefab;
    public LayerMask ExplosionMask;
    
    Vector3[] directions = new Vector3[8];
    Vector3 collisonPoint;
    public override void OnCollision(Collision collision)
    {
        soundSource.Play();
        collisonPoint = collision.contacts[0].point;

        Instantiate(ExplosionPrefab, collisonPoint+(collision.contacts[0].normal * 0.2f), Quaternion.identity);

        Collider [] colliders = Physics.OverlapSphere(collisonPoint, explosionRadius,ExplosionMask);
        List<HitboxComponent> hitboxes = new List<HitboxComponent>();


        foreach (var collider in colliders)
        {
            if (collider.TryGetComponent(out PlayerStats playerStats))
            {
                playerStats.ApplyDamage(damage * (1 / (Vector3.Distance(collisonPoint, collider.transform.position) + 0.1f)));
            }
        }
        foreach (var collider in colliders)
        {
            if (collider.GetComponent<HitboxComponent>()!=null)
            {
                hitboxes.Add(collider.GetComponent<HitboxComponent>());
            }
        }

        foreach (var hitbox in hitboxes)
        {
            hitbox.ApllyDamage(damage * (1 / (Vector3.Distance(collisonPoint, hitbox.transform.position) + 0.1f)));
        }

        foreach (var collider in colliders)
        {
            if (collider.TryGetComponent(out Rigidbody rb))
            {
                if (rb.isKinematic == false)
                {
                    rb.AddExplosionForce(100, collisonPoint, explosionRadius);
                }
            }
        }

        for (int i = 0; i < directions.Length; i++)
        {
            directions[i] = (-collision.contacts[0].normal
                + new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f))
                + -GetComponent<Rigidbody>().velocity.normalized/2).normalized;
        }
        for (int i = 0; i < directions.Length; i++)
        {
            SetBullet(Instantiate(shellPrefab, collisonPoint + collision.contacts[0].normal * 0.2f, Quaternion.identity), i);
        }
       
        GetComponent<SphereCollider>().enabled = false;
        Destroy(gameObject, 0.6f);
        //Debug.Break();
    }

    void SetBullet(GameObject bullet,int dirIndex)
    {
        bullet.GetComponent<Rigidbody>().AddForce((-directions[dirIndex]).normalized * shellSpeed, ForceMode.Impulse);
        Bullet tempBullet = bullet.GetComponent<Bullet>();
        tempBullet.SpeshalAction();
        tempBullet.damage = 18;
    }

    //private void OnDrawGizmos()
    //{
    //    for (int i = 0; i < directions.Length; i++)
    //    {
    //        Gizmos.DrawLine(collisonPoint,  collisonPoint- directions[i] );
    //        Gizmos.color = new Color32(255,0,0,30);
    //        Gizmos.DrawSphere(collisonPoint, explosionRadius);
    //    }
    //}
}
