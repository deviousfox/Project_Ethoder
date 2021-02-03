using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public ParticleSystem ParticleSystem;
    protected float damage;

    public void SetDamage(float damage)
    {
        this.damage = damage;
    }
    protected virtual void OnParticleCollision(GameObject other)
    {
           
    }
    protected virtual IEnumerator Shoot()
    {
        yield return null;
    }
}

