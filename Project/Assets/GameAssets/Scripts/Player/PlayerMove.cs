using Luminosity.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private CharacterController controller;


    public float StrafeRecovertSpeed = 1;
    private int strafeCount = 3;
    private float strafebar = 99;
    private bool isStrafeRecovery = false;


    [HideInInspector] public float gravity = 20.0f;
    float friction = 6.0f;

    public float MoveSpeed = 7.0f;
    public float MoveAcceleration = 14.0f;
    public float MoveDeacceleration = 10.0f;
    public float MoveInAirAcceleration = 2.0f;
    public float MoveInAirDeacceleration = 2.0f;
    public float InAirControl = 0.3f;
    public float SideStrafeAcceleration = 50f;
    public float SideStrafeSpeed = 1f;
    public float JumpSpeed = 8f;
    public bool BhopOn = false;

    public bool Grounded() => controller.isGrounded;

    private Vector3 moveDirection;
    private Vector3 moveDirectionNorm;
    public Vector3 playerVelocity;

    private bool wishJump = false;
    private float playerFriction = 0f;

    Vector3 playerSpawnPos;
    Quaternion playerSpawnRot;

    float zMove;
    float xMove;

    public delegate void UpdateStrafeEvet(float value);
    public static event UpdateStrafeEvet UpdateStrafeEventHendler;

    private void SetMoveDirection()
    {
        zMove = InputManager.GetAxis("Vertical");// Input.GetAxis("Vertical");
        xMove = InputManager.GetAxis("Horizontal");
    }

    private void Start()
    {

        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        
        Jump();
        Strafe();
        if (Grounded())
        {
            GroundMove();
        }
        else if (!Grounded())
        {
            AirMove();
        }

       
        controller.Move(playerVelocity * Time.deltaTime);
        LevelData.PlayerPosition = transform.position;
    }

    private void Strafe()
    {
        if (strafeCount < 3 && isStrafeRecovery == false)
        {
            StartCoroutine(RecoveryStrafe());
        }

        if (InputManager.GetButtonDown("Strafe") && strafeCount > 0)
        {
            Vector3 tempVector = playerVelocity.normalized;
            tempVector.y = 0;

            playerVelocity.y = 0;
            playerVelocity += tempVector * (MoveSpeed * MoveSpeed / 2);
            strafeCount -= 1;
            strafebar -= 32f;
        }
    }

    private void Jump()
    {
        if (BhopOn)
        {
            wishJump = InputManager.GetButtonDown("Jump");
            return;
        }

        if (InputManager.GetButtonDown("Jump") && !wishJump)
        {
            wishJump = true;
        }

        if (InputManager.GetButtonUp("Jump"))
        {
            wishJump = false;
        }
    }



    private void GroundMove()
    {
        Vector3 wishDirection;
        float wishSpeed;
        if (!wishJump)
        { ApplyFriction(1f); }
        else
        { ApplyFriction(0f); }
        SetMoveDirection();

        wishDirection = new Vector3(xMove, 0, zMove);
        wishDirection = transform.TransformDirection(wishDirection);
        wishDirection.Normalize();
        moveDirectionNorm = wishDirection;

        wishSpeed = wishDirection.magnitude;
        wishSpeed *= MoveSpeed;

        Accelerate(wishDirection, wishSpeed, MoveAcceleration);

        playerVelocity.y = -gravity * Time.deltaTime;

        if (wishJump)
        {
            playerVelocity.y = JumpSpeed;
            wishJump = false;
        }
    }

    private void AirMove()
    {
        Vector3 wishdir;
        float wishvel = MoveInAirAcceleration;
        float accel;

        SetMoveDirection();

        wishdir = new Vector3(xMove, 0, zMove);
        wishdir = transform.TransformDirection(wishdir);

        float wishspeed = wishdir.magnitude;
        wishspeed *= MoveSpeed;

        wishdir.Normalize();
        moveDirectionNorm = wishdir;


        float wishspeed2 = wishspeed;
        if (Vector3.Dot(playerVelocity, wishdir) < 0)
            accel = MoveInAirDeacceleration;
        else
            accel = MoveInAirAcceleration;

        if (zMove == 0 && xMove != 0)
        {
            if (wishspeed > SideStrafeSpeed)
                wishspeed = SideStrafeSpeed;
            accel = SideStrafeAcceleration;
        }

        Accelerate(wishdir, wishspeed, accel);
        if (InAirControl > 0)
            AirControl(wishdir, wishspeed2);

        playerVelocity.y -= gravity * Time.deltaTime;
    }

    private void AirControl(Vector3 wishDirection, float wishSpeed)
    {
        float ySpeed;
        float speed;
        float dot;
        float k;

        if (Mathf.Abs(zMove) < 0.001 || Mathf.Abs(wishSpeed) < 0.001)

            return;

        ySpeed = playerVelocity.y;
        playerVelocity.y = 0;
        speed = playerVelocity.magnitude;
        playerVelocity.Normalize();

        dot = Vector3.Dot(playerVelocity, wishDirection);
        k = 32;
        k *= InAirControl * Mathf.Sqrt(dot) * Time.deltaTime;

        if (dot > 0)
        {
            playerVelocity.x = playerVelocity.x * speed + wishDirection.x * k;
            playerVelocity.y = playerVelocity.y * speed + wishDirection.y * k;
            playerVelocity.z = playerVelocity.z * speed + wishDirection.z * k;

            playerVelocity.Normalize();
            moveDirectionNorm = playerVelocity;
        }
        playerVelocity.x *= speed;
        playerVelocity.y = ySpeed;
        playerVelocity.z *= speed;
    }


    private void ApplyFriction(float t)
    {
        float speed;
        float newSpeed;
        float control;
        float drop;

        playerVelocity.y = 0;

        speed = new Vector3(playerVelocity.x, 0, playerVelocity.z).magnitude;
        drop = 0;

        if (controller.isGrounded)
        {
            control = speed < MoveDeacceleration ? MoveDeacceleration : speed;
            drop = control * friction * Time.deltaTime * t;
        }

        newSpeed = speed - drop;
        playerFriction = newSpeed;
        if (newSpeed < 0)
        {
            newSpeed = 0;
        }
        else if (newSpeed > 0)
        {
            newSpeed /= speed;
        }
        playerVelocity.x *= newSpeed;
        playerVelocity.z *= newSpeed;
    }

    private void Accelerate(Vector3 wishDirection, float wishSpeed, float acceleration)
    {
        float addSpeed;
        float accelerationSpeed;
        float currentSpeed;

        currentSpeed = Vector3.Dot(playerVelocity, wishDirection);
        addSpeed = wishSpeed - currentSpeed;
        if (addSpeed <= 0)
        {
            return;
        }
        accelerationSpeed = acceleration * Time.deltaTime * wishSpeed;
        if (accelerationSpeed > addSpeed)
        {
            accelerationSpeed = addSpeed;
        }

        playerVelocity.x += accelerationSpeed * wishDirection.x;
        playerVelocity.z += accelerationSpeed * wishDirection.z;
    }

    private IEnumerator RecoveryStrafe()
    {
        
        isStrafeRecovery = true;
        while (strafebar < 99f)
        {
            strafebar += Time.deltaTime * StrafeRecovertSpeed;
            if ((int)strafebar%33==0)
            {
                strafeCount++;
                strafebar++;
            }
            UpdateStrafeEventHendler(strafebar);
            yield return null;
        }
        isStrafeRecovery = false;
        yield break;
                
    }
}
