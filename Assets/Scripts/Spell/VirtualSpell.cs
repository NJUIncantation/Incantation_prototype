using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.NJUCS.Spell
{
    public abstract class VirtualSpell : ScriptableObject
    {
        [Tooltip("释放对象")]
        protected GameObject Master;
        [Tooltip("目标对象,如果为非指向性技能则置null")]
        protected GameObject Target;
        [Tooltip("技能中心点")]
        protected Vector3 CastingPoint;
        [Tooltip("法力值消耗")]
        protected float ManaCost;
        [Tooltip("是否为伤害型技能")]
        protected bool Damageable;
        [Tooltip("技能CD")]
        protected float CoolDown;
        [Tooltip("技能CD")]
        protected float LastTimeCast = -1f;

        public virtual bool Cast()
        {
            return false;
        }


        public float CoolDownTimeLeft()
        {
            //冷却结束，返回0
            if (Time.time - LastTimeCast > CoolDown)
            {
                return 0;
            }
            //否则返回剩余时间
            else return Time.time - LastTimeCast;
        }

        public virtual bool SetMaster(GameObject go)
        {
            Master = go;
            return true;
        }

        public float GetManaCost() => ManaCost;
    }

}
