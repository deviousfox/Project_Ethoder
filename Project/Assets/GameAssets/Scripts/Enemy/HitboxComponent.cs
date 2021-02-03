using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxComponent : MonoBehaviour
{
    public float Health = 10;
    public EnemyHitboxType HitboxType;

    private Enemy thisEnemy;
    private void Start()
    {
       thisEnemy = GetComponentInParent<Enemy>();
    }
    public void ApllyDamage(float damage)
    {
        Health -= damage;
        if (Health <=0)
        {
            Instantiate(thisEnemy.HitboxDestroyParticle, transform.position, Quaternion.identity);
            //Send message to master enemy class if hitbox type != Common
            if ((HitboxType == EnemyHitboxType.Critical|| HitboxType == EnemyHitboxType.Main)&& thisEnemy!= null)
            {
                thisEnemy.EnemyDie();
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
