using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorPlatform : MonoBehaviour
{
    private ElevatorController elevatorController;
    void Start()
    {
        elevatorController = elevatorController ?? GetComponentInParent<ElevatorController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")&& elevatorController.CanInteract)
        {
            other.transform.SetParent(transform);
            elevatorController.Invoke("StartMoveUp", 0.1f); 
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(null);
        }
    }
}
