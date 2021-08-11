using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public CharacterData_SO enemyTemplateData;
    public CharacterData_SO characterData;
    public AttackData_SO attackData;
    [HideInInspector]
    public bool isCritical;

    private void Awake()
    {
        if(enemyTemplateData != null)
        {
            characterData = Instantiate(enemyTemplateData);
        }
    }

    #region Read from Data_SO

    public int MaxHealth
    {
        set
        {
            characterData.maxHealth = value;
        }
        get
        {
            if(characterData != null)
            {
                return characterData.maxHealth;
            }
            else return 0;
        }
    }
    public int CurrentHealth
    {
        set
        {
            characterData.currentHealth = value;
        }
        get
        {
            if(characterData != null)
            {
                return characterData.currentHealth;
            }
            else return 0;
        }
    }
    public int BaseDefence
    {
        set
        {
            characterData.baseDefence = value;
        }
        get
        {
            if(characterData != null)
            {
                return characterData.baseDefence;
            }
            else return 0;
        }
    }
    public int CurrentDefence
    {
        set
        {
            characterData.currentDefence = value;
        }
        get
        {
            if(characterData != null)
            {
                return characterData.currentDefence;
            }
            else return 0;
        }
    }
    #endregion

    #region Character Combat

    public void TakeDamage(CharacterStats attacker, CharacterStats defender)
    {
        int damage = Mathf.Max(attacker.CurrentDamage() - defender.CurrentDefence, 0);
        CurrentHealth = Mathf.Max(CurrentHealth - damage, 0);
        print("Take Damage!");
        if (attacker.isCritical)
        {
            print("set the trigger Hurt!");
            defender.GetComponent<Animator>().SetTrigger("Hurt");
        }

        //TODO: UI相关
    }

    private int CurrentDamage()
    {
        float coreDamage = UnityEngine.Random.Range(attackData.minDamage, attackData.maxDamage);
        
        if(isCritical)
        {
            coreDamage *= attackData.criticalMultiplier;
            Debug.Log("暴击" + coreDamage);
        }
        
        return (int)coreDamage;
    }

    #endregion


}
