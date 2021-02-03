using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class JumpPad : MonoBehaviour
{
    public float JumpSpeed;

    public bool isJump;

    private CinemachineDollyCart cart;
    private Transform cartTransform;
    private Transform playerTransform;

    private PlayerMove moveComponent;
    

    private void Start()
    {
        cart = cart ?? GetComponentInChildren<CinemachineDollyCart>();
        cartTransform = cart.transform;
        cart.m_Speed = 0;
        cart.m_Position = 0;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerTransform = playerTransform ?? other.transform;
            moveComponent = moveComponent ?? other.GetComponent<PlayerMove>();
            moveComponent.enabled = false;
            isJump = true;
        }
    }

    public void Update()
    {
        if (isJump)
        {
            cart.m_Speed = JumpSpeed;
            playerTransform.position = cartTransform.position;
            if (cart.m_Position >= 1f)
            {
                moveComponent.enabled = true;
                isJump = false;
                cart.m_Speed = 0;
                cart.m_Position = 0;
            }
        }
    }
}
