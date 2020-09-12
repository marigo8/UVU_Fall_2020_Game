using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
public class CharacterMover : MonoBehaviour
{
    private CharacterController controller;
    private MeshRenderer meshRenderer;

    public Text healthText;

    private string healthLabel;

    public bool useVehicleStyle;
    private bool leavingGround = false, invincible = false, dead = false;

    public float vehicleRotateSpeed = 120f, characterRotateSpeed = 10f, gravity = -9.81f, jumpForce = 30f, invincibleTime, spawnTime = 3f;
    private float yVar;

    private int jumpCount;
    private static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");
    
    private Vector3 movement;
    
    public FloatData moveSpeed, sprintModifier;

    public IntData playerJumpCount, playerHealth, playerMaxHealth;

    public Vector3Data currentSpawnPoint;

    public void TakeDamage(int damage)
    {
        if (invincible) return;
        playerHealth.value -= damage;
        StartCoroutine(nameof(FlashRed));
    }

    private void Die()
    {
        if (dead) return;
        dead = true;
        
        controller.enabled = false;
        meshRenderer.enabled = false;
        transform.GetChild(0).gameObject.SetActive(false);

        StartCoroutine(nameof(Respawn));
    }
    
    private void Start()
    {
        controller = GetComponent<CharacterController>();
        meshRenderer = GetComponent<MeshRenderer>();
        
        meshRenderer.material.shaderKeywords = new[] {"_EMISSION"};
        
        currentSpawnPoint.value = transform.position;

        playerHealth.value = playerMaxHealth.value;

        healthLabel = healthText.text;
    }

    private void Update()
    {
        if (!dead)
        {
            CheckHealth();
            Movement();
        }
    }
    
    private void CheckHealth()
    {
        healthText.text = healthLabel + playerHealth.value;
        
        if (playerHealth.value > 0) return;

        Die();
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

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(spawnTime);
        
        transform.position = currentSpawnPoint.value;
        playerHealth.value = playerMaxHealth.value;
        
        controller.enabled = true;
        meshRenderer.enabled = true;
        transform.GetChild(0).gameObject.SetActive(true);
        
        dead = false;
    }

    private IEnumerator FlashRed()
    {
        invincible = true;
        meshRenderer.material.SetColor(EmissionColor,Color.red * Mathf.LinearToGammaSpace(10f));
        
        yield return new WaitForSeconds(invincibleTime);
        
        meshRenderer.material.SetColor(EmissionColor,Color.black * Mathf.LinearToGammaSpace(10f));
        invincible = false;
    }
    
}
