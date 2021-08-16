using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Mana : MonoBehaviour
{
    [Tooltip("最大法力值")]
    public float MaxMana;

    [Tooltip("法力回复速度")]
    public float ManaRestoringSpeed = 0;

    public UnityAction<float, GameObject> OnManaGained;
    public UnityAction<float, GameObject> OnManaSpent;  

    private float m_CurrentMana;
    public float CurrentMana {
        get
        {
            return m_CurrentMana;
        }
    }

    public float GetRatio() => m_CurrentMana / MaxMana;

    private void Start()
    {
        m_CurrentMana = MaxMana;
    }

    private void Update()
    {
        m_CurrentMana = Mathf.Clamp(m_CurrentMana + ManaRestoringSpeed * Time.deltaTime, 0, MaxMana);
    }

    public bool IsFull() => Mathf.Abs(m_CurrentMana - MaxMana) < 0.01;

    public void ClearMana() => m_CurrentMana = 0;

    public void GainMana(float mana, GameObject source)
    {
        m_CurrentMana = Mathf.Clamp(m_CurrentMana + mana, 0, MaxMana);
        OnManaGained?.Invoke(mana, source);
    }

    public void ResetMana(float maxMana)
    {
        MaxMana = maxMana;
        m_CurrentMana = MaxMana;
    }

    public bool HaveEnoughMana(float manacost)
    {
        return manacost <= m_CurrentMana;
    }
    public bool SpendMana(float mana, GameObject spendon)
    {
        if(m_CurrentMana >= mana)
        {
            //Debug.Log("Spending mana");
            m_CurrentMana -= mana;
            OnManaSpent?.Invoke(mana, spendon);
            return true;
        }
        else
        {
            return false;
        }
    }

}
