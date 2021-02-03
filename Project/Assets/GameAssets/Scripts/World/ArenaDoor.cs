using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaDoor : Door
{
    public GameObject DoorObject;

    public override void Start()
    {
        
    }
    public override void Open()
    {
        DoorObject.SetActive(false);
    }
    public override void Close()
    {
        DoorObject.SetActive(true);
    }
}
