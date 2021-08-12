//Author: ������
//Last Modify: 2021.8.3

using UnityEngine;
using UnityEngine.Events;

namespace Unity.NJUCS.Game
{
    public class Health : MonoBehaviour
    {
        [Tooltip("�������ֵ")] 
        public float MaxHealth = 10f;

        [Tooltip("\"Σ��\"����ֵ����")]
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
        /// ���ƶ���ʱ����
        /// </summary>
        /// <param name="healAmount">������</param>
        /// <param name="healSource">�������ƵĶ���</param>
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
        /// ����˺�ʱ����
        /// </summary>
        /// <param name="damage">�˺���</param>
        /// <param name="damageSource">�˺���Դ</param>
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

            OnBecomeInvincible?.Invoke();
        }

        /// <summary>
        /// ֱ��ɱ������
        /// </summary>
        public void Kill()
        {
            CurrentHealth = 0f;

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
            if (CurrentHealth <= 0f)
            {
                m_IsDead = true;
                OnDie?.Invoke();
            }
        }
    }
}