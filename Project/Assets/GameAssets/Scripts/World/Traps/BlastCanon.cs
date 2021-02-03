using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BlastCanon : MonoBehaviour
{
    public ParticleSystem Fx;
    public GameObject bullet;
    public Transform bulletSpawn;

    public UnityEvent BeforeShoot;
    public UnityEvent AfterShoot;

   
    public void StartShoot()
    {
        StartCoroutine(Shoot());
    }

    private IEnumerator Shoot()
    {
        BeforeShoot?.Invoke();
        yield return new WaitForSeconds(1.5f);
        Fx.Play();
        Rigidbody rb = Instantiate(bullet, bulletSpawn).GetComponent<Rigidbody>();
        rb.AddForce((  bulletSpawn.position- transform.position) *3f, ForceMode.Impulse);
        rb.AddTorque(new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f), Random.Range(-10f, 10f)), ForceMode.Force);
        AfterShoot?.Invoke();
        yield break;
    }
}
