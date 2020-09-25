using System;
using System.Collections;
using System.Numerics;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

[RequireComponent(typeof(CharacterController))]
public class PlayerBehaviour : MonoBehaviour
{
    private CharacterController controller;
    private MeshRenderer meshRenderer;
    private CombatBehaviour combatBehaviour;

    [Header("UI")]
    [SerializeField] private Text healthText;
    private string healthLabel;

    [Header("Movement")]
    [SerializeField] private float vehicleRotateSpeed = 120f, characterRotateSpeed = 10f;
    [SerializeField] private FloatData moveSpeed, sprintModifier;
    private bool useVehicleStyle, playerCanMove;
    private Vector3 movement;
    private float moveSpeedModifier = 1f;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 30f;
    [SerializeField] private IntData playerJumpCount;
    private float yVar, jumpCountPowerUpTime;
    private int jumpCount, jumpCountModifier;

    [Header("Physics")] 
    [SerializeField] private float gravity = -9.81f;
    public Vector3 addedForce;
    private bool leavingGround = false;

    [Header("Health and Respawn")] 
    private HealthData health;
    //public IntData playerHealth;
    public TransformData currentSpawnPoint;
    [SerializeField] private float /*invincibleTime,*/ spawnTime = 3f;
    // [SerializeField] private IntData playerMaxHealth;
    private bool /*invincible = false,*/ dead = false;
    // private static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");
    
    // public void TakeDamage(int damage, Vector3 force)
    // {
    //     addedForce = force;
    //     if (invincible) return;
    //     health.health -= damage;
    //     StartCoroutine(nameof(Invincibility));
    // }
    private void Start()
    {
        playerCanMove = true;
        controller = GetComponent<CharacterController>();
        meshRenderer = GetComponent<MeshRenderer>();
        combatBehaviour = GetComponent<CombatBehaviour>();
        health = combatBehaviour.health;
        
        meshRenderer.material.shaderKeywords = new[] {"_EMISSION"};
        
        currentSpawnPoint.SetTransform(transform);

        // health.health = playerMaxHealth.value;

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
        healthText.text = healthLabel + health.health;
        
        if (health.health > 0) return;

        Die();
    }
    
    private void Movement()
    {
        GroundCheck();

        if (Input.GetKeyDown(KeyCode.V))
        {
            useVehicleStyle = !useVehicleStyle;
        }

        if (playerCanMove)
        {
            if (useVehicleStyle)
                MoveVehicleStyle();
            else
                MoveNormalStyle();

            movement *= moveSpeedModifier;

            Jump();
        }

        movement.y = yVar;
        
        AdditionalForce();

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
            yVar += gravity * Time.deltaTime;
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
        if (Input.GetButtonDown("Jump") && jumpCount < playerJumpCount.value+jumpCountModifier)
        {
            yVar = jumpForce;
            jumpCount++;
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
    
    private void Die()
    {
        if (dead) return;
        dead = true;

        addedForce = Vector3.zero;
        
        controller.enabled = false;
        meshRenderer.enabled = false;
        for (var i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        StartCoroutine(nameof(Respawn));
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(spawnTime);
        
        transform.position = currentSpawnPoint.position;
        transform.rotation = currentSpawnPoint.GetRotation();
        health.Initialize();

        controller.enabled = true;
        meshRenderer.enabled = true;
        
        for (var i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
        
        dead = false;
    }

    // private IEnumerator Invincibility()
    // {
    //     invincible = true;
    //     meshRenderer.material.SetColor(EmissionColor,Color.red * Mathf.LinearToGammaSpace(10f));
    //     
    //     yield return new WaitForSeconds(invincibleTime);
    //     
    //     meshRenderer.material.SetColor(EmissionColor,Color.black * Mathf.LinearToGammaSpace(10f));
    //     invincible = false;
    // }

    public void IncreaseJumpCount(PowerUpData powerUpData)
    {
        jumpCountModifier += powerUpData.intValue;
        StartCoroutine(DecreaseJumpCount(powerUpData));
    }
    private IEnumerator DecreaseJumpCount(PowerUpData powerUpData)
    {
        yield return new WaitForSeconds(powerUpData.time);
        jumpCountModifier -= powerUpData.intValue;
    }
    
    public void IncreaseMoveSpeed(PowerUpData powerUpData)
    {
        moveSpeedModifier += powerUpData.floatValue;
        StartCoroutine(DecreaseMoveSpeed(powerUpData));
    }
    private IEnumerator DecreaseMoveSpeed(PowerUpData powerUpData)
    {
        yield return new WaitForSeconds(powerUpData.time);
        moveSpeedModifier -= powerUpData.floatValue;
    }

    public void AddForce(Vector3 force)
    {
        addedForce += force;
    }
}
