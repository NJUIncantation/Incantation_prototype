//Author: 刘永鹏
//Last Modify: 2021.8.3

using UnityEngine;
using UnityEngine.Events;

namespace Unity.NJUCS.Game
{
    public class Health : MonoBehaviour
    {
        [Tooltip("最大生命值")] 
        public float MaxHealth = 10f;

        [Tooltip("\"危险\"生命值比例")]
        public float CriticalHealthRatio = 0.3f;

        
        public UnityAction<float, GameObject> OnDamaged;
        public UnityAction<float, GameObject> OnHealed;
        public UnityAction OnDie;
        public UnityAction OnBecomeInvincible;

        public float CurrentHealth { get; set; }
       

        public float GetRatio() => CurrentHealth / MaxHealth;
        public bool IsCritical() => GetRatio() <= CriticalHealthRatio;

        private bool m_IsDead;

        private bool m_Invincible;

        private float m_InvinvibleTime;

        private float m_BecomeInvincibleTime;

        void Start()
        {
            CurrentHealth = MaxHealth;
            m_Invincible = false;
        }
        /// <summary>
        /// 治疗对象时调用
        /// </summary>
        /// <param name="healAmount">治疗量</param>
        /// <param name="healSource">给予治疗的对象</param>
        public void Heal(float healAmount, GameObject healSource)
        {
            float healthBefore = CurrentHealth;
            CurrentHealth += healAmount;
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0f, MaxHealth);

            // call OnHeal action
            float trueHealAmount = CurrentHealth - healthBefore;
            if (trueHealAmount > 0f)
            {
                OnHealed?.Invoke(trueHealAmount, healSource);
            }
        }
        
        /// <summary>
        /// 造成伤害时调用
        /// </summary>
        /// <param name="damage">伤害量</param>
        /// <param name="damageSource">伤害来源</param>
        public void TakeDamage(float damage, GameObject damageSource)
        {
            if (Time.time - m_BecomeInvincibleTime > m_InvinvibleTime)
                m_Invincible = false;

            if (m_Invincible)
                return;

            float healthBefore = CurrentHealth;
            CurrentHealth -= damage;
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0f, MaxHealth);

            // call OnDamage action
            float trueDamageAmount = healthBefore - CurrentHealth;
            if (trueDamageAmount > 0f)
            {
                OnDamaged?.Invoke(trueDamageAmount, damageSource);
            }
            Debug.Log(message: "current health:" + CurrentHealth);
            HandleDeath();
        }

        /// <summary>
        /// 使对象变为无敌状态。若对象已经是无敌状态，重置其无敌持续时长
        /// </summary>
        /// <param name="time">无敌持续时间，单位：秒</param>
        public void BecomeInvincible(float time)
        {
            if (m_IsDead)
                return;

            m_InvinvibleTime = time;
            m_BecomeInvincibleTime = Time.time;
            m_Invincible = true;

            OnBecomeInvincible?.Invoke();
        }

        /// <summary>
        /// 直接杀死对象
        /// </summary>
        public void Kill()
        {
            CurrentHealth = 0f;

            // call OnDamage action
            OnDamaged?.Invoke(MaxHealth, null);

            HandleDeath();
        }

        /// <summary>
        /// 对象死亡时调用
        /// </summary>
        void HandleDeath()
        {
            if (m_IsDead)
                return;

            // call OnDie action
            if (CurrentHealth <= 0f)
            {
                m_IsDead = true;
                OnDie?.Invoke();
            }
        }
    }
}