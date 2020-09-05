using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(CharacterController))]
public class CharacterMover : MonoBehaviour
{
    private CharacterController controller;
    
    private Vector3 movement;

    public bool useVehicleStyle;

    public float moveSpeed = 5f, sprintModifier = 2f, vehicleRotateSpeed = 120f, characterRotateSpeed = 10f, gravity = -9.81f, jumpForce = 30f;
    private float yVar;

    public int jumpCountMax = 2;
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
        
        if (controller.isGrounded && movement.y <= 0)
        {
            yVar = 0f;
            jumpCount = 0;
        }
        
        if (useVehicleStyle)
            MoveVehicleStyle();
        else
            MoveNormalStyle();

        yVar += gravity * Time.deltaTime;

        

        if (Input.GetButtonDown("Jump") && jumpCount < jumpCountMax)
        {
            yVar = jumpForce;
            jumpCount++;
        }
        
        movement.y = yVar;

        controller.Move(movement * Time.deltaTime);
    }

    private void MoveVehicleStyle()
    {
        var vInput = Input.GetAxis("Vertical") * moveSpeed;
        if (Input.GetButton("Sprint"))
        {
            vInput *= sprintModifier;
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
        movement = Vector3.ClampMagnitude(movement, 1f) * moveSpeed;
        
        if (movement != Vector3.zero)
        {
            var rot = Quaternion.Lerp(transform.rotation,Quaternion.LookRotation(movement.normalized),characterRotateSpeed*Time.deltaTime);
            transform.rotation = rot;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            movement *= sprintModifier;
        }
    }

}
