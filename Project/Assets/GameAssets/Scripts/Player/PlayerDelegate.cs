using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDelegate : MonoBehaviour
{
    public delegate void OnPlayerSpawnetEvent(Transform tr);
    public static event OnPlayerSpawnetEvent OnPlayerSpawnetEventHandler;


    private void Start()
    {
        OnPlayerSpawnetEventHandler?.Invoke(transform);
    }
}
