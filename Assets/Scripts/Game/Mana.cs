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


    public float CurrentMana { set; get; }

    public float GetRatio() => CurrentMana / MaxMana;

    private void Start()
    {
        CurrentMana = MaxMana;
    }

    private void Update()
    {
        CurrentMana = Mathf.Clamp(CurrentMana + ManaRestoringSpeed * Time.deltaTime, 0, MaxMana);
    }

    public bool IsFull() => Mathf.Abs(CurrentMana - MaxMana) < 0.01;

    public void ClearMana() => CurrentMana = 0;

    public void GainMana(float mana, GameObject source)
    {
        CurrentMana = Mathf.Clamp(CurrentMana + mana, 0, MaxMana);
        OnManaGained?.Invoke(mana, source);
    }

    public bool SpendMana(float mana, GameObject spendon)
    {
        if(CurrentMana > mana)
        {
            CurrentMana -= mana;
            OnManaSpent?.Invoke(mana, spendon);
            return true;
        }
        else
        {
            return false;
        }
    }

}
