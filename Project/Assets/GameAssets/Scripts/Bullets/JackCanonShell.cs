using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JackCanonShell : Bullet
{
    public float MaxLifeTime;
    private int contactCount;
    private bool firstContact;
    MeshRenderer renderer_;
    TrailRenderer trailRenderer;

    Color emissionColor;

    private void Start()
    {
        renderer_ = GetComponent<MeshRenderer>();
        trailRenderer = GetComponent<TrailRenderer>();
        emissionColor = renderer_.material.GetColor("_EmissionColor");
    }

    public override void OnCollision(Collision collision)
    {
        if (collision.collider.TryGetComponent(out HitboxComponent hitbox) )
        {
            hitbox.ApllyDamage(damage);
            Destroy(gameObject);
        }
        if (collision.collider.TryGetComponent(out PlayerStats playerStats))
        {
            playerStats.ApplyDamage(damage);
            Destroy(gameObject);
        }

        if(firstContact== false)
        {
            StartCoroutine(LifeTime());
            firstContact = true;
        }

        contactCount++;
        if (contactCount <=3)
        {
            GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity / 2;
            damage = damage * 0.75f;
            renderer_.material.SetColor("_EmissionColor", emissionColor * (2.41f - 0.5f * contactCount));
            trailRenderer.time -= 0.25f;
        }
        else
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            renderer_.material.SetColor("_EmissionColor", Color.gray*1);
            damage = 1;
            Destroy(gameObject, 2f);
        }
        
    }

    public override void SpeshalAction()
    {
        Start();
        firstContact = true;
        StartCoroutine(LifeTime());
    }

    IEnumerator LifeTime()
    {
        float time =0;
        while (true)
        {
            time += Time.deltaTime;
            renderer_.material.SetColor("_EmissionColor", Color.Lerp(emissionColor, Color.gray,time) );
            trailRenderer.time = Mathf.Lerp(1, 0.1f, time*0.3f);
            if (time > MaxLifeTime)
            {
                Destroy(gameObject);
            }
            yield return null;
        }
    }
}
