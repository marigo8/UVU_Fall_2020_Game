using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterMover : MonoBehaviour
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
        Debug.Log(controller.isGrounded);
        Debug.Log(controller.velocity);
        
        walkingMovement.x = Input.GetAxis("Horizontal");
        walkingMovement.y = Input.GetAxis("Vertical");
        walkingMovement = Vector2.ClampMagnitude(walkingMovement, 1f) * moveSpeed; // so that moving diagonally doesn't make the character speed up
        movement.x = walkingMovement.x;
        movement.z = walkingMovement.y;
        
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

        controller.Move(movement*Time.deltaTime);
    }
}
