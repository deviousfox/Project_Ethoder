using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Liquid : MonoBehaviour
{
    public float GravitiInLiquid;
    private PlayerMove playerMove;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerMove = playerMove ?? other.GetComponent<PlayerMove>();

           // playerMove.InWaterMove = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           // playerMove.InWaterMove = false;
        }
    }
}
