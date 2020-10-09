using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterMover : MonoBehaviour
{
    private CharacterController controller;
    
    public bool canMove;
    
    [SerializeField] private IntData jumpCount;
    [SerializeField] private FloatData sprintModifier, stamina;
    [SerializeField] private float moveSpeed, rotateSpeed, jumpForce, staminaCooldownTime;
    
    private bool leavingGround, canSprint = true;
    private float yVar;
    private Vector3 movement, addedForce;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        stamina.SetValueToMax();
    }
    private void Update()
    {
        GroundCheck();
        if (canMove)
        {
            Movement();
        }
    }

    private void OnEnable()
    {
        addedForce = Vector3.zero;
    }
    private void GroundCheck()
    {
        if (controller.isGrounded && yVar <= 0)
        {
            //yVar = -1f;
            jumpCount.value = 0;
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

        Sprint();

        Jump();

        movement.y = yVar;
        
        AdditionalForce();

        controller.Move(movement * Time.deltaTime);
    }

    private void Sprint()
    {
        if (!canSprint) return;
        
        if (Input.GetButton("Sprint"))
        {
            if (stamina.value > 0)
            {
                movement *= sprintModifier.value;
                stamina.AddToValue(-Time.deltaTime);
            }
        }
        else
        {
            if (Input.GetButtonUp("Sprint"))
            {
                if (stamina.value <= 0)
                {
                    StartCoroutine(StaminaCooldown());
                    return;
                }
            }
            stamina.AddToValue(Time.deltaTime);
        }
        
    }
    private void Jump()
    {
        //if (stamina.value <= 0) return;
        if (Input.GetButtonDown("Jump") && jumpCount.value < jumpCount.maxValue)
        {
            yVar = jumpForce;
            jumpCount.AddToValue(1);
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

    private IEnumerator StaminaCooldown()
    {
        canSprint = false;
        yield return new WaitForSeconds(staminaCooldownTime);
        canSprint = true;
    }
}
