using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.NJUCS.Spell
{
    public abstract class VirtualSpell
    {
        [Tooltip("释放对象")]
        public GameObject Master;
        [Tooltip("目标对象,如果为非指向性技能则置null")]
        public GameObject Target;
        [Tooltip("技能中心点")]
        public Vector3 CastingPoint;
        [Tooltip("法力值消耗")]
        public float ManaCost;
        [Tooltip("是否为伤害型技能")]
        public bool Damageable;

        public virtual void Cast()
        {

        }
    }

}
