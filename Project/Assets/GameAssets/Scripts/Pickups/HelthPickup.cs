using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelthPickup : Puckup
{
    public float AmountHelth;
    private PlayerStats stats;
    private void Start()
    {
        stats = FindObjectOfType<PlayerStats>();
        if (stats == null)
        {
            stats = WorldState.PlayerStats;
        }
    }
    public override void Inititialize()
    {
        stats = WorldState.PlayerStats;
    }
    public override void Pickup()
    {
        if (stats.CanAddHelth())
        {
            stats.AddHelth(AmountHelth);
            PlaySound();
        }
    }
}
