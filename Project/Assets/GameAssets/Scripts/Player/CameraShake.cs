using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    // Transform of the camera to shake. Grabs the gameObject's transform
    // if null.
    private Transform camTransform;
    private Vector3 originalPos;

    // How long the object should shake for.
    public float ShakeDuration = 0f;

    // Amplitude of the shake. A larger value shakes the camera harder.
    public float ShakeAmount = 0.7f;
    public float DecreaseFactor = 1.0f;


    private void Awake()
    {
       // PlayerStats.ApplyDamageEventHandler += StartShake;
        if (camTransform == null)
        {
            camTransform = GetComponent<Transform>();
        }
    }

    private void OnEnable()
    {
        originalPos = camTransform.localPosition;
    }

    private  void Update()
    {
        if (ShakeDuration > 0)
        {
            Shake();
        }
        else
        {
            ShakeDuration = 0f;
            camTransform.localPosition = Vector3.Lerp(camTransform.localPosition, originalPos,10*Time.deltaTime) ;
        }
    }
    public void StartShake(float duration, float amount, float decriseFactor = 1f)
    {
        ShakeDuration = duration;
        ShakeAmount = amount;
        DecreaseFactor = decriseFactor;
    }
    private void Shake()
    {
        camTransform.localPosition = originalPos + Random.insideUnitSphere * ShakeAmount;

        ShakeDuration -= Time.deltaTime * DecreaseFactor;
    }
}