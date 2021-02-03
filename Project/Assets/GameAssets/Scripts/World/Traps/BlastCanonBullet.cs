using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastCanonBullet : MonoBehaviour
{
    public float Damage;
    public ParticleSystem Fx;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            WorldState.PlayerStats.ApplyDamage(Damage);
            Explosion();
        }    
    }

    public void Explosion()
    {
        Fx.Play();
        GetComponent<MeshCollider>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
        Destroy(gameObject, 1f);
    }
}
