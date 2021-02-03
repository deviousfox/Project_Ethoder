
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Damage;
    public HitboxComponent[] Hitboxes;
    public GameObject[] DestroyedableObjects;
    public GameObject HitboxDestroyParticle;

    public delegate void EnemyDieEvent();
    public static event EnemyDieEvent EnemyDieEventHandler;

    public bool isDie = false;

    public virtual void Start()
    {
    }

    
    public virtual void EnemyDie()
    {
        EnemyDieEventHandler?.Invoke();
        foreach (var Obj in DestroyedableObjects)
        {
            if (Obj != null)
            {
                Destroy(Obj);
            }
        }
    }
}
