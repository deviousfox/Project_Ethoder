using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sway : MonoBehaviour
{
    public float SwayAmount;
    public float SwaySmoothAmount;

    private Vector3 initializationPos;
    void Start()
    {
        initializationPos = transform.localPosition;
    }

    void Update()
    {
        Vector3 finalPos = new Vector3(-Input.GetAxis("Mouse X") * SwayAmount, -Input.GetAxis("Mouse Y") * SwayAmount, 0);
        transform.localPosition = Vector3.Lerp(transform.localPosition, finalPos + initializationPos, Time.deltaTime * SwaySmoothAmount);
    }
}
