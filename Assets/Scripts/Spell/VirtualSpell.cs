using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.NJUCS.Spell
{
    public abstract class VirtualSpell
    {
        [Tooltip("�ͷŶ���")]
        public GameObject Master;
        [Tooltip("Ŀ�����,���Ϊ��ָ���Լ�������null")]
        public GameObject Target;
        [Tooltip("�������ĵ�")]
        public Vector3 CastingPoint;
        [Tooltip("����ֵ����")]
        public float ManaCost;
        [Tooltip("�Ƿ�Ϊ�˺��ͼ���")]
        public bool Damageable;

        public virtual void Cast()
        {

        }
    }

}
