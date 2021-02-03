using Luminosity.IO;
using UnityEngine;

public class WeaponAnimationController : MonoBehaviour
{
    private Animator animator;
    public bool IsAutomatic;
    public WeaponType weaponType;

    private AmmoCounter ammoCounter;

    private void Start()
    {
        animator = GetComponent<Animator>();
        ammoCounter = FindObjectOfType<AmmoCounter>();
    }

    private void OnEnable()
    {
        
    }

    public void Hide()
    {
        animator.SetTrigger("Hide");
    }

    
    private void Update()
    {
        if (ammoCounter.CanDecriase(weaponType) )
        {
            if (IsAutomatic)
            {
                if (InputManager.GetButton("FirePrimary"))
                {
                    if (animator != null)
                    {
                        animator.SetTrigger("Primary");
                    }

                }
                if (InputManager.GetButton("FireSecondary"))
                {

                    if (animator != null)
                    {
                        animator.SetTrigger("Secondary");
                    }
                   // print("Secondary");
                }
            }
            else
            {
                if (InputManager.GetButtonDown("FirePrimary"))
                {
                    if (animator != null)
                    {
                        animator.SetTrigger("Primary");
                    }
                }
                if (InputManager.GetButtonDown("FireSecondary"))
                {

                    if (animator != null)
                    {
                        animator.SetTrigger("Secondary");
                    }
                  //  print("Secondary");
                }
            }
        }
        
    }
}
