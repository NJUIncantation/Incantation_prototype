//Author: ������
//Last Modify: 2021.8.3


using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Unity.NJUCS.Game
{
    public class DamageArea : MonoBehaviour
    {
        [Tooltip("DamageArea����뾶")]
        public float AreaOfEffectDistance = 0f;

        [Tooltip("DamageArea������")]
        public Vector3 Center = Vector3.zero;

        [Tooltip("DamageArea�������˺�")]
        public float Damage = 0f;

        [Tooltip("�˺���Դ")]
        public GameObject Owner = null;

        //DamageArea���˺�˥����ʽ��Ĭ����CenterΪ����
        public delegate float DamageDecayByDistance(Vector3 currentPosition);

        public void InflictDamageInArea(
            [UnityEngine.Internal.DefaultValue("AllLayers")] LayerMask layers,
            [UnityEngine.Internal.DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction interaction,
            DamageDecayByDistance damageDecayByDistance = null)
        {
            Dictionary<Health, Damageable> uniqueDamagedHealths = new Dictionary<Health, Damageable>();

            // Create a collection of unique health components that would be damaged in the area of effect (in order to avoid damaging a same entity multiple times)
            Collider[] affectedColliders = Physics.OverlapSphere(Center, AreaOfEffectDistance, layers, interaction);
            foreach (var coll in affectedColliders)
            {
                Damageable damageable = coll.GetComponent<Damageable>();
                if (damageable)
                {
                    Health health = damageable.GetComponentInParent<Health>();
                    if (health && !uniqueDamagedHealths.ContainsKey(health))
                    {
                        uniqueDamagedHealths.Add(health, damageable);
                    }
                }
            }

            // Apply damages with distance falloff
            foreach (Damageable uniqueDamageable in uniqueDamagedHealths.Values)
            {
                float damageAfterDecay = Damage;
                if (damageDecayByDistance != null)
                {
                    damageAfterDecay = damageDecayByDistance(uniqueDamageable.transform.position);
                }
                uniqueDamageable.InflictDamage(damageAfterDecay, true, Owner);
            }
        }

        private float DefaultDamageDecayByDistance(Vector3 currentPosition)
        {
            return Damage;
        }

        //void OnDrawGizmosSelected()
        //{
        //    Gizmos.color = AreaOfEffectColor;
        //    Gizmos.DrawSphere(transform.position, AreaOfEffectDistance);
        //}
    }
}