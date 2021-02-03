using Luminosity.IO;
using UnityEngine;

public class PlyerInteract : MonoBehaviour
{
    Camera mainCam;

    private void Start()
    {
        mainCam = GetComponentInChildren<Camera>();
    }

    void Update()
    {
        if (InputManager.GetButtonDown("Use"))
        {
            InteractObj tempCast = CastObj();
            if (tempCast != null && tempCast.CanDirectInteraction)
            {
                tempCast.OnInteractObject();
                print("YeeeBOY");
            }
            else
            {
                print("FUCKOFF");
            }
        }
    }

    private InteractObj CastObj()
    {
        Vector3 rayOrigin = mainCam.ViewportToWorldPoint(new Vector2(0.5f, 0.5f));
        Vector3 rayDirection = mainCam.transform.forward * 1.5f;
        RaycastHit rayHit = new RaycastHit();
        if (Physics.Raycast(rayOrigin,rayDirection, out rayHit,2f))
        {
            InteractObj tempObj = rayHit.collider.GetComponent<InteractObj>();
            if (tempObj != null)
            {
                return tempObj;
            }
            else return null;
        }
        else return null;
    }
}
