using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SicretObj : MonoBehaviour
{
    public bool IsFind;
    public int InstanceId;

    public void FindeSicret()
    {
        WorldState.SetSicretState(InstanceId);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FindeSicret();
            gameObject.SetActive(false);
        }
    }
}
