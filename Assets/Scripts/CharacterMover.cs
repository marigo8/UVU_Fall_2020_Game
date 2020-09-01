using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterMover : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 movement;
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
        Debug.Log(controller.isGrounded);
        
        movement.x = Input.GetAxis("Horizontal")*moveSpeed;
        movement.z = Input.GetAxis("Vertical")*moveSpeed;
        
        if (Input.GetButtonDown("Jump") && canJump)
        {
            if(!controller.isGrounded)
            {
                canJump = false;
            }
            movement.y = jumpForce;
            
        }

        if (controller.isGrounded)
        {
            //movement.y = 0;
            canJump = true;
        }
        else
        {
            movement.y += gravity * Time.deltaTime;
        }

        controller.Move(movement*Time.deltaTime);
    }
}
