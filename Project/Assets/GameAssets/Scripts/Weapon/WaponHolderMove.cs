using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Luminosity.IO;

public class WaponHolderMove : MonoBehaviour
{
    public float Intencity = 1f;
    public Transform WeaponHolder;
    public PlayerMove PlayerMove;
    #region HeadBobbing
    public float walkingBobbingSpeed = 14f;
    public float bobbingAmount = 0.05f;

    float defaultPosY = 0;
    float defaultPosX = 0;
    float timer = 0;
    #endregion


    void Start()
    {
        defaultPosY = WeaponHolder.localPosition.y;
        defaultPosX = WeaponHolder.localPosition.x;
    }

    void Update()
    {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, 4 * (-InputManager.GetAxis("Horizontal")*Intencity));

        if (Mathf.Abs(PlayerMove.playerVelocity.x) > 0.1f || Mathf.Abs(PlayerMove.playerVelocity.z) > 0.1f)
        {
            timer += Time.deltaTime * walkingBobbingSpeed;
            WeaponHolder.localPosition = new Vector3(defaultPosX + Mathf.Sin(timer) * bobbingAmount, defaultPosY + Mathf.Sin(timer) * bobbingAmount, WeaponHolder.localPosition.z);
        }
        else
        {
            timer = 0;
            WeaponHolder.localPosition = new Vector3(Mathf.Lerp(WeaponHolder.localPosition.x, defaultPosX, Time.deltaTime * walkingBobbingSpeed), Mathf.Lerp(WeaponHolder.localPosition.y, defaultPosY, Time.deltaTime * walkingBobbingSpeed), WeaponHolder.localPosition.z);
        }

    }
}
