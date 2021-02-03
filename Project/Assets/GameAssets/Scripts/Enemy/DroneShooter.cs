using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneShooter : EnemyShooter
{
    protected override void OnParticleCollision(GameObject other)
    {
        if (other.GetComponent<PlayerStats>()!= null)
        {
            other.GetComponent<PlayerStats>().ApplyDamage(damage);
        }
        //print(other.name);
    }
    protected override IEnumerator Shoot()
    {
        
        while (true)
        {
            if (Time.frameCount%240 ==0)
            {
                ParticleSystem.Play();
            }
            yield return null;
        }
    }
}
