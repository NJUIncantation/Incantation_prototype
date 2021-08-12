using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.NJUCS.Game;

namespace Unity.NJUCS.Widget
{
    public class FlyingObjectStandard : FlyingObjectBase
    {
        [Header("General")]
        [Tooltip("弹丸碰撞检测的半径")]
        public float Radius = 0.01f;

        //[Tooltip("飞行物根的变换（用于碰撞检测）")]
        //public Transform Root;

        [Tooltip("飞行物顶点的变换（用于碰撞检测）")]
        public Transform Tip;

        [Tooltip("子弹生存时间")]
        public float MaxLifeTime = 5f;


        [Header("Movement")]
        [Tooltip("子弹速度")]
        public float Speed = 20f;

        [Tooltip("子弹受到的重力加速度")]
        public float GravityDownAcceleration = 0f;

        [Tooltip("是否计算武器本身的速度")]
        public bool InheritWeaponVelocity = false;

        [Header("Damage")]
        [Tooltip("飞行物伤害")]
        public float Damage = 40f;

        [Tooltip("伤害范围")]
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


