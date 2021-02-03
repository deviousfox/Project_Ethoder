using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float MaxHelth = 100;
    [SerializeField] private float MaxArmor = 100;

    public float CurrentHelth = 100;
    public float CurrentArmor = 0;

    public delegate void UpdateHealthEvent(int helth);
    public delegate void UpdateArmorEvent(int armor);
    public static event UpdateHealthEvent UpdateHelthEventHandler;
    public static event UpdateArmorEvent UpdateArmorEventHandler;

   // public delegate void ApplyDamageEvent(float time, float amount, float d);
    //public static event ApplyDamageEvent ApplyDamageEventHandler;

    public void ApplyDamage(float damage)
    {
        if (CurrentArmor > 0)
        {
            CurrentArmor -= damage * .66f;
            CurrentHelth -= damage * .25f;
            if (CurrentArmor < 0)
                CurrentArmor = 0;            

            Mathf.RoundToInt(CurrentArmor);
            Mathf.RoundToInt(CurrentHelth);
        }
        else
        {
            CurrentHelth -= damage;
            Mathf.RoundToInt(CurrentHelth);
        }

        if (CurrentHelth <=0)
        {
            CurrentHelth = 0;
            Death();
        }

        UpdateHelthEventHandler((int)CurrentHelth);
        UpdateArmorEventHandler((int)CurrentArmor);
       // ApplyDamageEventHandler(1, 0.1f,1);
    }

    public void AddArmor(float armor)
    {
        if (CurrentArmor + armor > MaxArmor)
        {
            CurrentArmor = MaxArmor;
        }
        else
        {
            CurrentArmor += armor;
        }
        UpdateArmorEventHandler((int)CurrentArmor);
    }
    public void AddHelth(float helth)
    {
        if (CurrentHelth + helth > MaxHelth)
        {
            CurrentHelth = MaxHelth;
        }
        else
        {
            CurrentHelth += helth;
        }
        UpdateHelthEventHandler((int)CurrentHelth);
    }

    public bool CanAddHelth() => CurrentHelth < MaxHelth;
    
    public bool CanAddArmor() => CurrentArmor < MaxArmor;

    private void Start()
    {
        UpdateArmorEventHandler((int)CurrentArmor);
        UpdateHelthEventHandler((int)CurrentHelth);
    }
    
    private void Death()
    {
        Debug.LogError("YOU DIE");
    }
}
