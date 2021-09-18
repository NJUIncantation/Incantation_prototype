using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.NJUCS.NPC
{
    [CreateAssetMenu(menuName = "Attack/Attack Data", fileName = "New Attack")]
    public class AttackData_SO : ScriptableObject
    {
        [Header("Attack Info")]
        public float minDamage;
        public float maxDamage;
        public float attackRange;
        public float skillRange;
        public float baseDefence;
        public float coolDown;
        public float criticalChance;
        public float criticalMultiplier;
    }
}

