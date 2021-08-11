using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.NJUCS.Camera;
//[RequireComponent(typeof(MainCameraController))]
namespace Unity.NJUCS.Character
{
    public class PlayerCharacter : MonoBehaviour
    {
        float forwardInput;
        float rightInput;
        public MainCameraController mainCameraController;
        public CharacterMovement characterMovement;
        public CharacterAnimationController characterAnimationController;
        private Vector3 velocity;
        // Start is called before the first frame update
        void Start()
        {
            //mainCameraController =GetComponent<MainCameraController>();
            //characterMovement.SetMovementMode(MovementMode.Walking);
            //characterAnimationController.SetMovementMode(MovementMode.Walking);
        }

        // Update is called once per frame
        void Update()
        {
            //transform.Translate(velocity);
        }
        public void UpdatePosition(float forward, float right)
        {
            forwardInput = forward;
            rightInput = right;

            Vector3 camFwd = mainCameraController.transform.forward;
            Vector3 camRight = mainCameraController.transform.right;
            Vector3 tarMov = forwardInput * camFwd + rightInput * camRight;
            tarMov.y = 0;
            velocity = tarMov.magnitude > 0 ? tarMov : Vector3.zero;
            characterMovement.Velocity = tarMov;
        }

        public void Jump()
        {
            characterMovement.Jump();
            characterAnimationController.Jump();
        }

        public void ToggleRun()
        {
            if (characterMovement.GetMovementMode() == MovementMode.Running)
            {
                characterMovement.SetMovementMode(MovementMode.Walking);
                characterAnimationController.SetMovementMode(MovementMode.Walking);
            }
            else
            {
                characterMovement.SetMovementMode(MovementMode.Running);
                characterAnimationController.SetMovementMode(MovementMode.Running);
            }
        }
        public void ToggleCrouch()
        {
            if (characterMovement.GetMovementMode() == MovementMode.Crouching)
            {
                characterMovement.SetMovementMode(MovementMode.Walking);
                characterAnimationController.SetMovementMode(MovementMode.Walking);
            }
            else
            {
                characterMovement.SetMovementMode(MovementMode.Crouching);
                characterAnimationController.SetMovementMode(MovementMode.Crouching);
            }
        }
        public void ToggleSprint()
        {
            if (characterMovement.GetMovementMode() == MovementMode.Sprinting)
            {
                characterMovement.SetMovementMode(MovementMode.Running);
                characterAnimationController.SetMovementMode(MovementMode.Running);
            }
            else
            {
                characterMovement.SetMovementMode(MovementMode.Sprinting);
                characterAnimationController.SetMovementMode(MovementMode.Sprinting);
            }
        }
        public float getVelocity()
        {
            //Debug.Log(characterMovement.Velocity.magnitude);
            return characterMovement.Velocity.magnitude;
        }
    }
}