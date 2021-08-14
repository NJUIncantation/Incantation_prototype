using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.NJUCS.Spell
{
    public abstract class VirtualSpell : ScriptableObject
    {
        [Tooltip("�ͷŶ���")]
        protected GameObject Master;
        [Tooltip("Ŀ�����,���Ϊ��ָ���Լ�������null")]
        protected GameObject Target;
        [Tooltip("�������ĵ�")]
        protected Vector3 CastingPoint;
        [Tooltip("����ֵ����")]
        protected float ManaCost;
        [Tooltip("�Ƿ�Ϊ�˺��ͼ���")]
        protected bool Damageable;
        [Tooltip("����CD")]
        protected float CoolDown;
        [Tooltip("����CD")]
        protected float LastTimeCast = -1f;

        public virtual bool Cast()
        {
            return false;
        }


        public float CoolDownTimeLeft()
        {
            //��ȴ����������0
            if (Time.time - LastTimeCast > CoolDown)
            {
                return 0;
            }
            //���򷵻�ʣ��ʱ��
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
