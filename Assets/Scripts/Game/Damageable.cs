//Author: 刘永鹏
//Last Modify: 2021.8.3

using UnityEngine;
using System.Collections;

namespace Unity.NJUCS.Game
{
    [RequireComponent(typeof(Health))]
    public class Damageable : MonoBehaviour
    {
        [Tooltip("伤害系数")]
        public float DamageMultiplier = 1f;

        [Range(0, 1)]
        [Tooltip("自伤系数")]
        public float SelfdamageMultiplier = 0f;

        public Health Health { get; private set; }

        void Awake()
        {
            // find the health component either at the same level, or higher in the hierarchy
            Health = GetComponent<Health>();
            if (!Health)
            {
                Health = GetComponentInParent<Health>();
            }
        }
        /// <summary>
        /// 该函数将伤害区域造成的伤害进行处理后施加到Health组件上。
        /// </summary>
        /// <param name="damage"></param>
        /// <param name="isExplosionDamage"></param>
        /// <param name="damageSource"></param>
        public void InflictDamage(float damage, bool isExplosionDamage, GameObject damageSource)
        {
            if (Health)
            {
                if (damage < 0)
                    return;

                float totalDamage = damage;

                // skip the crit multiplier if it's from an explosion
                if (!isExplosionDamage)
                {
                    totalDamage *= DamageMultiplier;
                }

                // potentially reduce damages if inflicted by self
                if (Health.gameObject == damageSource)
                {
                    totalDamage *= SelfdamageMultiplier;
                }

                // apply the damages
                Health.TakeDamage(totalDamage, damageSource);
            }
        }

    }
}