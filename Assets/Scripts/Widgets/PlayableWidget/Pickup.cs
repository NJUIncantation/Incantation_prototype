using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.NJUCS.Character;

namespace Unity.NJUCS.Widget
{
    [RequireComponent(typeof(Rigidbody), typeof(Collider))]
    public class Pickup : MonoBehaviour
    {
        [Tooltip("Frequency at which the item will move up and down")]
        public float VerticalBobFrequency = 1f;

        [Tooltip("Distance the item will move up and down")]
        public float BobbingAmount = 1f;

        [Tooltip("捡拾道具每秒转动的角度")]
        public float RotatingSpeed = 360f;

        public Rigidbody PickupRigidbody { get; private set; }

        Vector3 m_StartPosition;
        Collider m_Collider;

        protected virtual void Start()
        {
            PickupRigidbody = GetComponent<Rigidbody>();
            m_Collider = GetComponent<Collider>();

            m_StartPosition = transform.position;
        }

        private void Update()
        {
            // Handle bobbing
            float bobbingAnimationPhase = ((Mathf.Sin(Time.time * VerticalBobFrequency) * 0.5f) + 0.5f) * BobbingAmount;
            transform.position = m_StartPosition + Vector3.up * bobbingAnimationPhase;

            // Handle rotating
            transform.Rotate(Vector3.up, RotatingSpeed * Time.deltaTime, Space.Self);
        }

        void OnTriggerEnter(Collider other)
        {
            PlayerCharacter pickingPlayer = other.GetComponent<PlayerCharacter>();
            if (pickingPlayer != null)
            {
                OnPicked(pickingPlayer);
            }
        }
        protected virtual void OnPicked(PlayerCharacter player)
        {
            
        }

    }
}

