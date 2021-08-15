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

        [Tooltip("��������ı任��������ײ��⣩")]
        public Transform Root;

        [Tooltip("�����ﶥ��ı任��������ײ��⣩")]
        public Transform Tip;

        [Tooltip("�ӵ�����ʱ��")]
        public float MaxLifeTime = 5f;

        [Tooltip("��ײ����LayerMask")]
        public LayerMask HittableLayers = LayerMask.NameToLayer("AllLayer");


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
        public float AreaOfEffect = 1.0f;

        
        DamageArea AreaOfDamage;

        FlyingObjectBase m_FlyingObjectBase;
        Vector3 m_LastRootPosition;
        Vector3 m_Velocity;
        float m_ShootTime;
        List<Collider> m_IgnoredColliders;

        const QueryTriggerInteraction k_TriggerInteraction = QueryTriggerInteraction.Collide;

        //ʵ����ʱ������
        void OnEnable()
        {
            AreaOfDamage = GetComponent<DamageArea>();
            m_FlyingObjectBase = GetComponent<FlyingObjectBase>();
            m_FlyingObjectBase.OnShoot += OnShoot;
            Destroy(gameObject, MaxLifeTime);

            // Ignore colliders of owner
            m_IgnoredColliders = new List<Collider>();
            Collider[] ownerColliders = m_FlyingObjectBase.Owner.GetComponentsInChildren<Collider>();
            m_IgnoredColliders.AddRange(ownerColliders);
        }

        new void OnShoot()
        {
            m_ShootTime = Time.time;
            m_LastRootPosition = Root.position;
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

            // Hit detection
            {
                RaycastHit closestHit = new RaycastHit();
                closestHit.distance = Mathf.Infinity;
                bool foundHit = false;

                // Sphere cast
                Vector3 displacementSinceLastFrame = Tip.position - m_LastRootPosition;
                RaycastHit[] hits = Physics.SphereCastAll(m_LastRootPosition, Radius,
                    displacementSinceLastFrame.normalized, displacementSinceLastFrame.magnitude, HittableLayers,
                    k_TriggerInteraction);
                foreach (var hit in hits)
                {
                    if (IsHitValid(hit) && hit.distance < closestHit.distance)
                    {
                        foundHit = true;
                        closestHit = hit;
                    }
                }

                if (foundHit)
                {
                    // Handle case of casting while already inside a collider
                    if (closestHit.distance <= 0f)
                    {
                        closestHit.point = Root.position;
                        closestHit.normal = -transform.forward;
                    }

                    OnHit();
                }
            }

            m_LastRootPosition = Root.position;
        }

        bool IsHitValid(RaycastHit hit)
        {
            // ignore hits with triggers that don't have a Damageable component
            if (hit.collider.isTrigger && hit.collider.GetComponent<Damageable>() == null)
            {
                return false;
            }

            // ignore hits with specific ignored colliders (self colliders, by default)
            if (m_IgnoredColliders != null && m_IgnoredColliders.Contains(hit.collider))
            {
                return false;
            }

            return true;
        }

        void OnHit()
        {
            // area damage
            AreaOfDamage.Center = Tip.position;
            AreaOfDamage.AreaOfEffectDistance = AreaOfEffect;
            AreaOfDamage.Owner = m_FlyingObjectBase.Owner;
            AreaOfDamage.Damage = Damage;
            AreaOfDamage.InflictDamageInArea(HittableLayers, k_TriggerInteraction, 
                (Vector3 currentPostion) => {
                    float damage = AreaOfDamage.Damage * ((1 - (currentPostion - AreaOfDamage.Center).magnitude) / AreaOfDamage.AreaOfEffectDistance);
                    return damage > 0 ? damage : 0;
                }
                );

            // Self Destruct
            Destroy(this.gameObject);

        }

    }
}


