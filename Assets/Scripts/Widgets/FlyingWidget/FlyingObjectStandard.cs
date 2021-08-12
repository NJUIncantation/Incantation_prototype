using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.NJUCS.Game;

namespace Unity.NJUCS.Widget
{
    public class FlyingObjectStandard : FlyingObjectBase
    {
        [Header("General")]
        [Tooltip("������ײ���İ뾶")]
        public float Radius = 0.01f;

        //[Tooltip("��������ı任��������ײ��⣩")]
        //public Transform Root;

        [Tooltip("�����ﶥ��ı任��������ײ��⣩")]
        public Transform Tip;

        [Tooltip("�ӵ�����ʱ��")]
        public float MaxLifeTime = 5f;


        [Header("Movement")]
        [Tooltip("�ӵ��ٶ�")]
        public float Speed = 20f;

        [Tooltip("�ӵ��ܵ����������ٶ�")]
        public float GravityDownAcceleration = 0f;

        [Tooltip("�Ƿ��������������ٶ�")]
        public bool InheritWeaponVelocity = false;

        [Header("Damage")]
        [Tooltip("�������˺�")]
        public float Damage = 40f;

        [Tooltip("�˺���Χ")]
        public DamageArea AreaOfDamage;

        FlyingObjectBase m_FlyingObjectBase;
        Vector3 m_Velocity;
        float m_ShootTime;

        void OnEnable()
        {
            m_FlyingObjectBase = GetComponent<FlyingObjectBase>();
            m_FlyingObjectBase.OnShoot += OnShoot;
            Destroy(gameObject, MaxLifeTime);
        }

        new void OnShoot()
        {
            m_ShootTime = Time.time;
            m_Velocity = transform.forward * Speed;
            transform.position += m_FlyingObjectBase.InheritedMuzzleVelocity * Time.deltaTime;
        }

        void Update()
        {
            // Move
            transform.position += m_Velocity * Time.deltaTime;
            if (InheritWeaponVelocity)
            {
                transform.position += m_FlyingObjectBase.InheritedMuzzleVelocity * Time.deltaTime;
            }

            // Gravity
            if (GravityDownAcceleration > 0)
            {
                // add gravity to the projectile velocity for ballistic effect
                m_Velocity += Vector3.down * GravityDownAcceleration * Time.deltaTime;
            }

        }

    }
}


