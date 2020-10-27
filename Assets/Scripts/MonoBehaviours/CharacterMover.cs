using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterMover : MonoBehaviour
{
    public bool canMove;
    public IntData jumpCount;
    public FloatData sprintModifier, slowModifier, stamina;
    public float moveSpeed, rotateSpeed, jumpForce, staminaCooldownTime, staminaReplenishTime;
    
    private bool leavingGround, staminaCoolingDown, jumpButtonDown, sprintButtonDown;
    private float speedModifier = 1f, yVar;
    private Vector3 movement, addedForce;
    private CharacterController controller;
    
    private Coroutine sprintCoroutine;
    private readonly WaitForFixedUpdate fixedWait = new WaitForFixedUpdate();
    private WaitForSeconds cooldownWait;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        cooldownWait = new WaitForSeconds(staminaCooldownTime);
        stamina.SetValueToMax();
    }
    private void FixedUpdate()
    {
        GroundCheck();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            jumpButtonDown = true;
        }

        if (Input.GetButtonDown("Sprint"))
        {
            sprintButtonDown = true;
        }
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
            yVar += Physics.gravity.y * Time.fixedDeltaTime;
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

        if (sprintButtonDown)
        {
            sprintButtonDown = false;
            
            if (!staminaCoolingDown)
            {
                if (sprintCoroutine != null) // if a coroutine is already running...
                {
                    StopCoroutine(sprintCoroutine); // stop coroutine
                }

                sprintCoroutine = StartCoroutine(Sprint()); // start coroutine
            }
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
        if (!jumpButtonDown) return;
        
        jumpButtonDown = false;
        
        if (jumpCount.value >= jumpCount.maxValue) return;
        
        yVar = jumpForce;
        jumpCount.AddToValue(1);
    }

    private void AdditionalForce()
    {
        addedForce = Vector3.Lerp(addedForce, Vector3.zero, 5 * Time.fixedDeltaTime);
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
