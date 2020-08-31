using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterMover : MonoBehaviour
{
    private CharacterController _controller;
    private Vector3 _movement;
    public float gravity = -9.81f;
    public float moveSpeed = 3f;
    public float sprintModifier = 1.5f;
    public float jumpForce = 10f;
    public int jumpCountMax;
    public float rotateSpeed = 3f;
    private Vector3 rotateMovement;
    
    private void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        rotateMovement.y = rotateSpeed * Input.GetAxis("Horizontal");
        transform.Rotate(rotateMovement);
        
        _movement.x = Input.GetAxis("Horizontal")*moveSpeed;
        _movement.z = Input.GetAxis("Vertical")*moveSpeed;
        
        if (Input.GetButtonDown("Jump"))
        {
            _movement.y = jumpForce;
        }

        if (_controller.isGrounded)
        {
            _movement.y = 0;
        }
        else
        {
            _movement.y += gravity * Time.deltaTime;
        }

        _movement = transform.TransformDirection(_movement);
        _controller.Move(_movement*Time.deltaTime);
    }
}
