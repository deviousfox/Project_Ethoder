using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnTrigger : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerMove>().enabled = false;
            RespawnController.Respawn(other.gameObject);
            other.GetComponent<PlayerMove>().enabled = true;
        }
    }
}
