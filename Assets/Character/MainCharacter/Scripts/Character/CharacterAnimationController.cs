using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    public Animator animator;
    private PlayerCharacter playerCharacter;
    private float speed;
    // Start is called before the first frame update
    void Start()
    {
        playerCharacter = GetComponent<PlayerCharacter>();
        //Time.timeScale = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (animator == null)
        {
            Debug.LogWarning("no valid animator");
        }
        speed = Mathf.SmoothStep(speed, playerCharacter.getVelocity(), Time.deltaTime*20);
        animator.SetFloat("Velocity", speed);
        //Debug.Log(speed);
    }
    public void SetMovementMode(MovementMode mode)
    {

        switch (mode)
        {
            case MovementMode.Walking:
                animator.SetInteger("MovementState", 0);
                break;
            case MovementMode.Running:
                animator.SetInteger("MovementState", 0);
                break;
            case MovementMode.Crouching:
                animator.SetInteger("MovementState", 1);
                break;
            case MovementMode.Swimming:
                animator.SetInteger("MovementState", 2);
                break;
            case MovementMode.Sprinting:
                animator.SetInteger("MovementState", 3);
                break;
        }
    }

    internal void Jump()
    {
        animator.SetTrigger("Jump");
    }
}
