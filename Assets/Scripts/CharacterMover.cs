using System;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(CharacterController))]
public class CharacterMover : MonoBehaviour
{
    private CharacterController controller;
    
    private Vector3 movement;

    public bool useVehicleStyle;
    private bool leavingGround = false;

    public float vehicleRotateSpeed = 120f, characterRotateSpeed = 10f, gravity = -9.81f, jumpForce = 30f;
    private float yVar;

    public FloatData moveSpeed, sprintModifier;

    public IntData playerJumpCount;

    public Vector3Data currentSpawnPoint;

    private int jumpCount;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            useVehicleStyle = !useVehicleStyle;
        }
        
        if (controller.isGrounded && yVar <= 0)
        {
            //yVar = -1f;
            jumpCount = 0;
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
        }
        
        if (useVehicleStyle)
            MoveVehicleStyle();
        else
            MoveNormalStyle();

        yVar += gravity * Time.deltaTime;

        

        if (Input.GetButtonDown("Jump") && jumpCount < playerJumpCount.value)
        {
            yVar = jumpForce;
            jumpCount++;
        }
        
        movement.y = yVar;

        controller.Move(movement * Time.deltaTime);
    }

    private void MoveVehicleStyle()
    {
        var vInput = Input.GetAxis("Vertical") * moveSpeed.value;
        if (Input.GetButton("Sprint"))
        {
            vInput *= sprintModifier.value;
        }

        movement.Set(0, 0, vInput);

        var hInput = Input.GetAxis("Horizontal") * vehicleRotateSpeed * Time.deltaTime;
        transform.Rotate(0, hInput, 0);
        movement = transform.TransformDirection(movement);
    }

    private void MoveNormalStyle()
    {
        var hInput = Input.GetAxis("Horizontal");
        var vInput = Input.GetAxis("Vertical");
        movement.Set(hInput, 0, vInput);
        movement = Vector3.ClampMagnitude(movement, 1f) * moveSpeed.value;
        
        if (movement != Vector3.zero)
        {
            var rot = Quaternion.Lerp(transform.rotation,Quaternion.LookRotation(movement.normalized),characterRotateSpeed*Time.deltaTime);
            transform.rotation = rot;
        }

        if (Input.GetButton("Sprint"))
        {
            movement *= sprintModifier.value;
        }
    }

    
    private void OnEnable()
    {
        // Set the position of the player to the location data of the player.
    }
}
