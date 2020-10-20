using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterMover : MonoBehaviour
{
    private CharacterController controller;
    
    public bool canMove;
    
    public ClampedIntData jumpCount;
    public FloatData sprintModifier, slowModifier;
    public ClampedFloatData stamina;
    public float moveSpeed, rotateSpeed, jumpForce, staminaCooldownTime, staminaReplenishTime;
    
    private bool leavingGround, canSprint = true, staminaCoolingDown;
    private float speedModifier = 1f, yVar;
    private Vector3 movement, addedForce;
    private Coroutine sprintCoroutine;
    
    private WaitForFixedUpdate fixedWait = new WaitForFixedUpdate();
    private WaitForSeconds cooldownWait;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        cooldownWait = new WaitForSeconds(staminaCooldownTime);
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
        movement = Vector3.ClampMagnitude(movement, 1f) * (moveSpeed * speedModifier);
        
        if (movement != Vector3.zero)
        {
            var rot = Quaternion.Lerp(transform.rotation,Quaternion.LookRotation(movement.normalized),rotateSpeed*Time.deltaTime);
            transform.rotation = rot;
        }

        if (Input.GetButtonDown("Sprint") && !staminaCoolingDown)
        {
            if (sprintCoroutine != null) // if a coroutine is already running...
            {
                StopCoroutine(sprintCoroutine); // stop coroutine
            }
            sprintCoroutine = StartCoroutine(Sprint()); // start coroutine
        }

        Jump();

        movement.y = yVar;
        
        AdditionalForce();

        controller.Move(movement * Time.deltaTime);
    }
    
    private IEnumerator Sprint()
    {
        // Sprinting Speed
        speedModifier = sprintModifier.value;
        
        // Deplete Stamina
        while (Input.GetButton("Sprint") && stamina.value > 0)
        {
            stamina.AddToValue(-Time.fixedDeltaTime);
            yield return fixedWait;
        }
        
        // Stop Sprinting
        if (stamina.value > 0)
        {
            // Regular Speed
            speedModifier = 1f;
            yield return cooldownWait;
        }
        else
        {
            // Slow Speed
            staminaCoolingDown = true;
            speedModifier = slowModifier.value;
            yield return cooldownWait;
        }

        // Regenerate Stamina
        while (!stamina.IsMaxed)
        {
            stamina.AddToValue((stamina.maxValue / staminaReplenishTime) * Time.fixedDeltaTime);
            yield return fixedWait;
        }
        // End
        speedModifier = 1f;
        staminaCoolingDown = false;
    }
    
    private void Jump()
    {
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
}
