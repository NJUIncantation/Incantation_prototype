using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Unity.NJUCS.Widget
{
    [RequireComponent(typeof(Rigidbody), typeof(Collider))]
    public class Pickup : MonoBehaviour
    {
        [Tooltip("��ʰ����ÿ��ת���ĽǶ�")]
        public float RotatingSpeed = 360f;

        public Rigidbody PickupRigidbody { get; private set; }

        Collider m_Collider;

        protected virtual void Start()
        {
            PickupRigidbody = GetComponent<Rigidbody>();
            m_Collider = GetComponent<Collider>();

        }

    }
}

