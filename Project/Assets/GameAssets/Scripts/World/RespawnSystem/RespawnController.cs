using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnController : MonoBehaviour
{
    public static Transform LastCheckPoint;

    public static void Respawn(GameObject obj)
    {
        if (LastCheckPoint != null)
        {
            obj.transform.position = LastCheckPoint.position;
        }
        else
        {
            obj.transform.position = Vector3.zero;
        }
    }
}
