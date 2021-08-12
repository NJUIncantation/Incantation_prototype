using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.NJUCS.Game;
using Unity.NJUCS.Camera;
//[RequireComponent(typeof(MainCameraController))]
namespace Unity.NJUCS.Character
{
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(Damageable))]
    public class PlayerCharacter : MonoBehaviour
    {
        float forwardInput;
        float rightInput;
        public MainCameraController mainCameraController;
        public CharacterMovement characterMovement;
        public CharacterAnimationController characterAnimationController;
        public string characterName;

        private Vector3 velocity;
        private ActorManager m_actorManager;
        private Health health;
        private Damageable damageable;
        // Start is called before the first frame update
        void Start()
        {
            m_actorManager = FindObjectOfType<ActorManager>();
            characterName = "mainCharacter";
            health = gameObject.GetComponent<Health>();
            damageable = gameObject.GetComponent<Damageable>();
            if(health == null)
            {
                Debug.LogError("Health Component Not Found");
            }
            if(damageable = null)
            {
                Debug.LogError("Damagable Component Not Found");
            }
            m_actorManager.CreateActor(characterName, gameObject);
        }

        private void OnDestroy()
        {
            m_actorManager.DeleteActor(characterName);
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

        public void OnDie()
        {
            EventManager.Broadcast(Events.PlayerDeathEvent);
        }
    }
}