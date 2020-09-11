using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
public class CharacterMover : MonoBehaviour
{
    private CharacterController controller;

    public Text healthText;

    private string healthLabel;

    public bool useVehicleStyle;
    private bool leavingGround = false, defaultSpawnPointSet = false;

    public float vehicleRotateSpeed = 120f, characterRotateSpeed = 10f, gravity = -9.81f, jumpForce = 30f;
    private float yVar;

    private int jumpCount;
    
    private Vector3 movement;
    
    public FloatData moveSpeed, sprintModifier;

    public IntData playerJumpCount, playerHealth, playerMaxHealth;

    public Vector3Data currentSpawnPoint;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        
        currentSpawnPoint.value = transform.position;
        defaultSpawnPointSet = true;

        playerHealth.value = playerMaxHealth.value;

        healthLabel = healthText.text;
    }

    private void Update()
    {
        print(controller.velocity.y);
        Movement();
        UpdateUI();
    }
    
    private void OnEnable()
    {
        if (defaultSpawnPointSet)
        {
            transform.position = currentSpawnPoint.value;
        }
    }
    
    private void Movement()
    {
        GroundCheck();

        if (Input.GetKeyDown(KeyCode.V))
        {
            useVehicleStyle = !useVehicleStyle;
        }

        if (useVehicleStyle)
            MoveVehicleStyle();
        else
            MoveNormalStyle();

        yVar += gravity * Time.deltaTime;

        Jump();
        
        movement.y = yVar;

        controller.Move(movement * Time.deltaTime);
    }
    
    private void GroundCheck()
    {
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

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && jumpCount < playerJumpCount.value)
        {
            yVar = jumpForce;
            jumpCount++;
        }
    }

    private void UpdateUI()
    {
        healthText.text = healthLabel + playerHealth.value;
    }
    
}
