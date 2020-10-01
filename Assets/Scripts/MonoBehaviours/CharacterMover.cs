using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterMover : MonoBehaviour
{
    private CharacterController controller;
    
    public bool canMove;
    
    [SerializeField] private IntData jumpCount;
    [SerializeField] private FloatData sprintModifier;
    [SerializeField] private float moveSpeed, rotateSpeed, jumpForce;
    
    private bool leavingGround;
    private float yVar;
    private Vector3 movement, addedForce;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }
    private void Update()
    {
        GroundCheck();
        if (canMove)
        {
            Movement();
        }
    }
    private void GroundCheck()
    {
        if (controller.isGrounded && yVar <= 0)
        {
            //yVar = -1f;
            jumpCount.SetZero();
            leavingGround = true;
        }
        else
        {
            if(leavingGround){
                leavingGround = false;
                if (yVar < 0)
                {
                    yVar = 0;
                }
            }
            yVar += Physics.gravity.y * Time.deltaTime;
        }
    }
    private void Movement()
    {
        var hInput = 0f;
        var vInput = 0f;
        if (canMove)
        {
            hInput = Input.GetAxis("Horizontal");
            vInput = Input.GetAxis("Vertical");
        }
        movement.Set(hInput, 0, vInput);
        movement = Vector3.ClampMagnitude(movement, 1f) * moveSpeed;
        
        if (movement != Vector3.zero)
        {
            var rot = Quaternion.Lerp(transform.rotation,Quaternion.LookRotation(movement.normalized),rotateSpeed*Time.deltaTime);
            transform.rotation = rot;
        }

        if (Input.GetButton("Sprint"))
        {
            movement *= sprintModifier.value;
        }

        Jump();

        movement.y = yVar;
        
        AdditionalForce();

        controller.Move(movement * Time.deltaTime);
    }
    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && jumpCount.value < jumpCount.maxValue)
        {
            yVar = jumpForce;
            jumpCount.UpdateValue(1);
        }
    }

    private void AdditionalForce()
    {
        addedForce = Vector3.Lerp(addedForce, Vector3.zero, 5 * Time.deltaTime);
        if (addedForce.magnitude <0.01f)
        {
            addedForce = Vector3.zero;
        }

        if (addedForce.magnitude > 0)
        {
            movement += addedForce;
        }
    }
    
    public void AddForce(Vector3 force)
    {
        addedForce += force;
    }
}
