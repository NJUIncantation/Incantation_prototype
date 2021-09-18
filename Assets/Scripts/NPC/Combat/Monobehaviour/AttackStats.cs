using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Unity.NJUCS.NPC
{
    public class AttackStats : MonoBehaviour
    {
        public bool isCritical;
        public AttackData_SO AttackData;

        #region Get Attack Data from ATKData_SO
        public float MinATK
        {
            get
            {
                if (AttackData == null) return 0;
                else return AttackData.minDamage;
            }
        }
        public float MaxATK
        {
            get
            {
                if (AttackData == null) return 0;
                else return AttackData.maxDamage;
            }
        }
        public float BaseDefence
        {
            get
            {
                if (AttackData == null) return 0;
                else return AttackData.baseDefence;
            }
        }
        public float CriticalChance
        {
            get
            {
                if (AttackData == null) return 0;
                else return AttackData.criticalChance;
            }
        }
        public float CriticalMultiplier
        {
            get
            {
                if (AttackData == null) return 0;
                else return AttackData.criticalMultiplier;
            }
        }
        public float ATKRange
        {
            get
            {
                if (AttackData == null) return 0;
                else return AttackData.attackRange;
            }
        }
        public float SkillRange
        {
            get
            {
                if (AttackData == null) return 0;
                else return AttackData.skillRange;
            }
        }
        public float CoolDown
        {
            get
            {
                if (AttackData == null) return 0;
                else return AttackData.coolDown;
            }
        }
        #endregion

    }
}

