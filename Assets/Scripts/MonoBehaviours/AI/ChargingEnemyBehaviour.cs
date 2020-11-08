using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class ChargingEnemyBehaviour : MonoBehaviour
{
    public Transform target;
    public float chargingSpeed = 10f, aimingTime = 5f, chargingTime;

    private CharacterController controller;
    private Vector3 movement, gravityForce;

    private readonly WaitForFixedUpdate fixedWait = new WaitForFixedUpdate();
    private WaitForSeconds chargingWait;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        chargingWait = new WaitForSeconds(chargingTime);
        
        StartCoroutine(Aim());
    }

    private void Update()
    {
        controller.Move((movement + gravityForce) * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        Gravity();
    }
    
    private void Gravity()
    {
        // Gravity
        if (controller.isGrounded)
        {
            gravityForce.y = 0;
        }
        gravityForce.y += Physics.gravity.y * Time.fixedDeltaTime;
    }

    private IEnumerator Aim()
    {
        var timeElapsed = 0f;
        while (timeElapsed < aimingTime)
        {
            transform.LookAt(target);
            var rotation = transform.eulerAngles;
            rotation.x = 0f;
            transform.eulerAngles = rotation;
            
            yield return fixedWait;
            timeElapsed += Time.fixedDeltaTime;
        }

        StartCoroutine(Charge());
    }

    private IEnumerator Charge()
    {
        movement = transform.forward * chargingSpeed;
        yield return chargingWait;
        movement = Vector3.zero;

        StartCoroutine(Aim());
    }
}
