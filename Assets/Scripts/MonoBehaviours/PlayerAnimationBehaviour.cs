using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerMoveBehaviour))]
public class PlayerAnimationBehaviour : MonoBehaviour
{
    private Animator anim;
    private PlayerMoveBehaviour playerMove;

    private void Start()
    {
        anim = GetComponent<Animator>();
        playerMove = GetComponent<PlayerMoveBehaviour>();
    }

    private void Update()
    {
        anim.SetBool("Walking", playerMove.isWalking);
        anim.SetBool("Running", playerMove.isRunning);
    }
}
