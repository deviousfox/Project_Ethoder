
using UnityEngine;

public class PlayerLook : MonoBehaviour
{

    public Transform PlayerCam;
    public float CamYOffset = 0.6f;
    public float MouseSensivity = 30.0f;

    private float CamRotX;
    private float CamRotY;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    void Update()
    {
        if (Cursor.lockState != CursorLockMode.Locked)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }

        CamRotX -= Input.GetAxisRaw("Mouse Y") * MouseSensivity * 0.02f;
        CamRotY += Input.GetAxisRaw("Mouse X") * MouseSensivity * 0.02f;

        CamRotX = Mathf.Clamp(CamRotX, -90, 90);

        transform.rotation = Quaternion.Euler(0, CamRotY, 0);
        PlayerCam.rotation = Quaternion.Euler(CamRotX, CamRotY, PlayerCam.rotation.eulerAngles.z);
    }
}
