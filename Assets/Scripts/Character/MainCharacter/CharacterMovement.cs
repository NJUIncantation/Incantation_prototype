using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Unity.NJUCS.Character
{
   
    public enum MovementMode { Walking, Running, Crouching, Swimming, Sprinting }
    public class CharacterMovement : MonoBehaviour
    {
        private MovementMode movementMode;
        private Vector3 velocity;
        private float jumpForce;
        public Transform t_mesh;

        public float maxSpeed;
        public float smoothSpeed;
        public float rotationSpeed;

        public float walkSpeed;
        public float runSpeed;
        public float sprintSpeed;
        public float crouchSpeed;
        private Rigidbody m_rigidbody;
        private Vector3 actualVelocity;
        private Vector3 characterPosition;

        public delegate void OnLandedDelegate();
        public event OnLandedDelegate onLanded;

        private bool inAir;
        // Start is called before the first frame update
        void Start()
        {
            maxSpeed = 0;
            rotationSpeed = 11;
            smoothSpeed = 0;
            walkSpeed = 2.5f;
            runSpeed = 5f;
            sprintSpeed = 8f;
            crouchSpeed = 3f;
            jumpForce = 300;
            SetMovementMode(MovementMode.Walking);
            m_rigidbody = GetComponent<Rigidbody>();
            characterPosition = transform.position;
            inAir = false;

        }

        // Update is called once per frame
        void Update()
        {

            actualVelocity = Vector3.Lerp(actualVelocity, (transform.position - characterPosition) / Time.deltaTime, Time.deltaTime * 5);
            characterPosition = transform.position;
            //Debug.Log(actualVelocity.magnitude);
            if (velocity.magnitude > 0)
            {
                //Debug.Log(velocity.magnitude);
                m_rigidbody.velocity = new Vector3(velocity.normalized.x * smoothSpeed, GetComponent<Rigidbody>().velocity.y, velocity.normalized.z * smoothSpeed);
                smoothSpeed = Mathf.Lerp(smoothSpeed, maxSpeed, Time.deltaTime);
                t_mesh.rotation = Quaternion.Lerp(t_mesh.rotation, Quaternion.LookRotation(velocity), Time.deltaTime * rotationSpeed);

            }
            else
            {
                smoothSpeed = 0;// Mathf.Lerp(smoothSpeed, 0, Time.deltaTime);
            }
            if(inAir)
            {
                //Debug.Log("inAir");
                if (Physics.Linecast(transform.position + new Vector3(0, 0.1f, 0), transform.position + new Vector3(0, -0.1f, 0)))
                {
                    inAir = false;
                    //Debug.Log("onlanded");
                    onLanded();
                }
            }

        }

        internal void Jump()
        {
            m_rigidbody.AddForce(Vector3.up * jumpForce);
            inAir = true;
        }

        public void SetMovementMode(MovementMode mode)
        {
            movementMode = mode;
            switch (mode)
            {
                case MovementMode.Walking:
                    maxSpeed = walkSpeed;
                    break;
                case MovementMode.Running:
                    maxSpeed = runSpeed;
                    break;
                case MovementMode.Crouching:
                    maxSpeed = crouchSpeed;
                    break;
                case MovementMode.Swimming:
                    break;
                case MovementMode.Sprinting:
                    maxSpeed = sprintSpeed;
                    break;
            }
        }
        public MovementMode GetMovementMode()
        {
            return movementMode;
        }
        public Vector3 Velocity { get => actualVelocity; set => velocity = value; }
    }
}