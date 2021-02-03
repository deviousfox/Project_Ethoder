using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : InteractObj
{
    [Header("Door parametrs")]
    [Range(0.2f,10f)] public float DoorSpeedMultiplier = 1;
    private Animator animator;
    public bool OpenOnStart;
    public MeshRenderer DoorIndicator;
    public Color CanInteractColor = Color.green , CantInteractColor = Color.red;
    

    public virtual void Start()
    {
            animator = GetComponent<Animator>();
            animator.speed = DoorSpeedMultiplier;
        if (OpenOnStart)
            Open();
        if (CanInteract)
        {
            DoorIndicator.material.SetColor("_EmissionColor", CanInteractColor * 2.41f);
        }
        else { DoorIndicator.material.SetColor("_EmissionColor", CantInteractColor * 2.41f); } 
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")&& CanInteract)
        {
            Open();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Close();
        }
    }

    public virtual void Open()
    { 
        animator?.SetBool("Open", true);
    }

    public virtual void Close()
    {
        animator?.SetBool("Open", false);
    }


    public override void AllowInteract()
    {
        CanInteract = true;
        DoorIndicator.material.SetColor("_EmissionColor", CanInteractColor * 2.41f);
    }
    public override void DisallowInteract()
    {
        CanInteract = false;
        DoorIndicator.material.SetColor("_EmissionColor", CantInteractColor * 2.41f);
    }
}
