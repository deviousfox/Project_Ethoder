using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class Puckup : MonoBehaviour
{
    
    public bool Animate;
    [Range(0,1)]public float YBorder = 0.5f;
    [Range(0f,10f)]public float HorizontalSpeed;
    [Range(0.2f,10f)] public float Intenciviti = 1;

    public AudioClip PickupClip;

    private AudioSource source;
    private Transform thisTransform;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Pickup();
        }  
    }

    private void Start()
    {
        Invoke("Inititialize", 1f);
        thisTransform = transform;
        source = GetComponent<AudioSource>();
        if (Animate)
        {
            StartCoroutine(AnimatePickup());
        }
    }

    public virtual void Inititialize()
    {

    }

    public virtual void Pickup()
    {

    }
    public void PlaySound()
    {
        if (source != null)
        {
            source.pitch = Random.Range(0.7f, 1.3f);
            source.PlayOneShot(PickupClip);
        }
        else
        {
        }
    }
    private IEnumerator AnimatePickup()
    {
        while (true)
        {
            thisTransform.localPosition = Vector3.Lerp(thisTransform.localPosition, new Vector3(thisTransform.localPosition.x, Mathf.Clamp((Mathf.Sin(Time.deltaTime)*10)*Intenciviti, 1 -YBorder,1 +YBorder),thisTransform.position.z), HorizontalSpeed * Time.deltaTime) ;
            if (Time.renderedFrameCount % 15 == 0)
            {
                transform.Rotate(0,5,0);
            }
            yield return null;
        }
    }
}
