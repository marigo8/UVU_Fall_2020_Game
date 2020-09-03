using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(CharacterController))]
public class CharacterMover : MonoBehaviour
{
    private CharacterController controller;
    
    private Vector3 movement;

    [FormerlySerializedAs("useCarStyle")] public bool useVehicleStyle;

    public float moveSpeed = 5f, sprintModifier = 2f, rotateSpeed = 30f, gravity = -9.81f, jumpForce = 30f;
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
            transform.SetPositionAndRotation(transform.position, Quaternion.identity);
        }
        
        if (useVehicleStyle)
        {
            MoveVehicleStyle();
            movement = transform.TransformDirection(movement);
        }
        else
        {
            MoveNormalStyle();
        }

        yVar += gravity * Time.deltaTime;

        if (controller.isGrounded && movement.y <= 0)
        {
            yVar = -1f;
            jumpCount = 0;
        }

        if (Input.GetButtonDown("Jump") && jumpCount < jumpCountMax)
        {
            yVar = jumpForce;
            jumpCount++;
        }



        controller.Move(movement * Time.deltaTime);
    }

    private void MoveVehicleStyle()
    {
        var vInput = Input.GetAxis("Vertical") * moveSpeed;
        if (Input.GetButton("Sprint"))
        {
            vInput *= sprintModifier;
        }

        movement.Set(0, yVar, vInput);

        var hInput = Input.GetAxis("Horizontal") * rotateSpeed * Time.deltaTime;
        transform.Rotate(0, hInput, 0);
    }

    private void MoveNormalStyle()
    {
        var hInput = Input.GetAxis("Horizontal");
        var vInput = Input.GetAxis("Vertical");
        movement.Set(hInput, 0, vInput);
        movement = Vector3.ClampMagnitude(movement, 1f) * moveSpeed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            movement *= sprintModifier;
        }
        movement.y = yVar;
    }

}
