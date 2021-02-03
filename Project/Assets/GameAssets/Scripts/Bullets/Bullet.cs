using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public AudioClip soundClip;
    public AudioSource soundSource;

    private void Start()
    {
        soundSource = GetComponent<AudioSource>();
    }

    public  void OnCollisionEnter(Collision collision)
    {
        OnCollision(collision);
    }

    public virtual void OnCollision(Collision collision)
    {

    }
    public virtual void SpeshalAction()
    {

    }
}
