using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New AttackData", menuName = "Attack/Attack Data", order = 0)]
public class AttackData_SO : ScriptableObject 
{
    public float attackRange;
    public float skillRange;
    public float coolDown;
    public int minDamage;
    public int maxDamage;
    public float criticalChance;
    public float criticalMultiplier; 

}

