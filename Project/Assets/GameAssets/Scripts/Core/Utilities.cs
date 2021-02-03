using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities : MonoBehaviour
{
    List<TimerComponent> timers;
    private void Update()
    {
        
    }
}
public struct TimerComponent
{
    public float TimerTotal;
    public float TimerDecrease;
    public bool IsFinished;
}
