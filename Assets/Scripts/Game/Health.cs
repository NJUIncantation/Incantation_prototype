//Author: 刘永鹏
//Last Modify: 2021.8.3

using UnityEngine;
using UnityEngine.Events;

namespace Unity.NJUCS.Game
{
    public class Health : MonoBehaviour
    {
        [Tooltip("最大生命值")] 
        public float MaxHealth = 100f;

        [Tooltip("\"危险\"生命值比例")]
        public float CriticalHealthRatio = 0.3f;

        [Tooltip("最大护盾值")]
        public float MaxShield = 10f;

        [Tooltip("护盾初始值")]
        public float StartingShield = 0;

        [Tooltip("当前护盾值")]
        [SerializeField]
        private float m_CurrentShield = 0f;

        [Tooltip("生命回复速度: 生命/秒")]
        private float HealingSpeed = 0;

        
        public UnityAction<float, GameObject> OnDamaged;
        public UnityAction<float, GameObject> OnHealed;
        public UnityAction<float> OnBecomeInvincible;
        public UnityAction OnDie;
        public UnityAction OnRespawn;

        private float m_CurrentHealth;

        public float CurrentHealth { get; }
        public float CurrentShield { get; }


        public float GetRatio() => m_CurrentHealth / MaxHealth;
        public bool IsCritical() => GetRatio() <= CriticalHealthRatio;

        public bool IsFull() => Mathf.Abs(m_CurrentHealth - MaxHealth) < 0.01;

        private bool m_IsDead;

        private bool m_Invincible;

        private float m_InvinvibleTime;

        private float m_BecomeInvincibleTime;

        void Start()
        {
            m_CurrentHealth = MaxHealth;
            m_CurrentShield = StartingShield;
            m_Invincible = false;
        }

        private void Update()
        {
            if (m_Invincible && (Time.time - m_BecomeInvincibleTime > m_InvinvibleTime))
                m_Invincible = false;

            m_CurrentHealth = Mathf.Clamp(m_CurrentHealth + HealingSpeed * Time.deltaTime, 0, MaxHealth);

        }
        /// <summary>
        /// 治疗对象时调用
        /// </summary>
        /// <param name="healAmount">治疗量</param>
        /// <param name="healSource">给予治疗的对象</param>
        public void Heal(float healAmount, GameObject healSource)
        {
            float healthBefore = m_CurrentHealth;
            m_CurrentHealth += healAmount;
            m_CurrentHealth = Mathf.Clamp(m_CurrentHealth, 0f, MaxHealth);

            // call OnHeal action
            float trueHealAmount = m_CurrentHealth - healthBefore;
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

            if (m_Invincible)
                return;

            Debug.Log(gameObject + " 's health is damaged by " + damageSource);

            if(m_CurrentShield > 0)
            {
                damage -= m_CurrentShield;
                if (m_CurrentShield < 0)
                    m_CurrentShield = 0;
                if (damage <= 0)
                    return;
            }

            float healthBefore = m_CurrentHealth;
            m_CurrentHealth -= damage;
            m_CurrentHealth = Mathf.Clamp(m_CurrentHealth, 0f, MaxHealth);

            // call OnDamage action
            float trueDamageAmount = healthBefore - m_CurrentHealth;
            if (trueDamageAmount > 0f)
            {
                OnDamaged?.Invoke(trueDamageAmount, damageSource);
            }
            Debug.Log(message: "current health:" + m_CurrentHealth);
            HandleDeath();
        }
        
        public void GainShield(float amount)
        {
            m_CurrentShield = Mathf.Clamp(amount + m_CurrentShield, 0, MaxShield);
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

            OnBecomeInvincible?.Invoke(time);
        }


        public void Respawn()
        {
            m_CurrentHealth = MaxHealth;
            m_CurrentShield = StartingShield;
            m_Invincible = false;
            OnRespawn?.Invoke();
        }

        /// <summary>
        /// 直接杀死对象
        /// </summary>
        public void Kill()
        {
            m_CurrentHealth = 0f;

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
            if (m_CurrentHealth <= 0f && !m_Invincible)
            {
                m_IsDead = true;
                OnDie?.Invoke();
            }
        }
    }
}