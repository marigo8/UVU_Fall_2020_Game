﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Temp : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 movement;
    private Vector2 walkingMovement;
    public float gravity = -9.81f;
    public float moveSpeed = 3f;
    public float sprintModifier = 1.5f;
    public float jumpForce = 10f;
    public bool canJump;
    
    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    
    private void Update()
    {
        Walk();
        JumpAndGravity();
        controller.Move(movement*Time.deltaTime);
    }
    
    private void Walk()
    {
        walkingMovement.x = Input.GetAxis("Horizontal");
        walkingMovement.y = Input.GetAxis("Vertical");
        walkingMovement = Vector2.ClampMagnitude(walkingMovement, 1f) * moveSpeed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            walkingMovement *= sprintModifier;
        }
        movement.x = walkingMovement.x;
        movement.z = walkingMovement.y;
    }

    private void JumpAndGravity()
    {
        movement.y += gravity * Time.deltaTime;
        if (Input.GetButtonDown("Jump") && canJump)
        {
            if(!controller.isGrounded)
            {
                canJump = false;
            }
            movement.y = jumpForce;
            
        }
        else if (controller.isGrounded)
        {
            movement.y = -1;
            canJump = true;
            
        }
    }
}