using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyConfig : MonoBehaviour
{
    private EnemyHealth enemyHealth;

    private void Start()
    {
        enemyHealth = ScriptableObject.CreateInstance<EnemyHealth>();
    }

    private void OnTriggerEnter(Collider other)
    {
        enemyHealth.value -= 0.1f;
    }
}
