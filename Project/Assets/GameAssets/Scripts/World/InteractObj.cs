using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.Events;


public class InteractObj : MonoBehaviour
{
    public bool CanDirectInteraction;
    public bool CanInteract;
    public UnityAction action;

    public virtual void OnInteractObject()
    {
        action?.Invoke();
    }
    public virtual void AllowInteract()
    {

    }
    public virtual void DisallowInteract()
    {

    }
}
