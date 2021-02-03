using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIDataHolder : MonoBehaviour
{
    public static UIDataHolder Instance;

    private void Awake()
    {
        Instance = this;
    }
    
}
