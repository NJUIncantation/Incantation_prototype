//Author: ������
//Last Modify: 2021.8.3

using UnityEngine;
using UnityEngine.Events;

namespace Unity.NJUCS.Game
{
    public class Health : MonoBehaviour
    {
        [Tooltip("�������ֵ")] 
        public float MaxHealth = 100f;

        [Tooltip("\"Σ��\"����ֵ����")]
        public float CriticalHealthRatio = 0.3f;

        [Tooltip("��󻤶�ֵ")]
        public float MaxShield = 10f;

        [Tooltip("���ܳ�ʼֵ")]
        public float StartingShield = 0;

        [Tooltip("��ǰ����ֵ")]
        [SerializeField]
        private float m_CurrentShield = 0f;

        [Tooltip("�����ظ��ٶ�: ����/��")]
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
        /// ���ƶ���ʱ����
        /// </summary>
        /// <param name="healAmount">������</param>
        /// <param name="healSource">�������ƵĶ���</param>
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
        /// ����˺�ʱ����
        /// </summary>
        /// <param name="damage">�˺���</param>
        /// <param name="damageSource">�˺���Դ</param>
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
        /// ʹ�����Ϊ�޵�״̬���������Ѿ����޵�״̬���������޵г���ʱ��
        /// </summary>
        /// <param name="time">�޵г���ʱ�䣬��λ����</param>
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
        /// ֱ��ɱ������
        /// </summary>
        public void Kill()
        {
            m_CurrentHealth = 0f;

            // call OnDamage action
            OnDamaged?.Invoke(MaxHealth, null);

            HandleDeath();
        }

        /// <summary>
        /// ��������ʱ����
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