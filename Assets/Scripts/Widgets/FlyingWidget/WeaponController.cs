using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.NJUCS.Widget
{
    public enum WeaponShootType
    {
        Manual,
        Automatic,
        Charge,
    }

    public class WeaponController : MonoBehaviour
    {
        [Header("Information")]
        [Tooltip("���������ֻ���UI����ʾ")]
        public string WeaponName;

        [Header("Internal References")]
        //[Tooltip("The root object for the weapon, this is what will be deactivated when the weapon isn't active")]
        //public GameObject WeaponRoot;

        [Tooltip("���ǹ�ڵ�λ��")]
        public Transform WeaponMuzzle;

        [Header("Shoot Parameters")]
        [Tooltip("The type of weapon wil affect how it shoots")]
        public WeaponShootType ShootType;

        [Tooltip("�ӵ�Ԥ����")]
        public FlyingObjectBase FlyingObjectPrefab;

        public GameObject Owner { get; set; }           //��¼˭ӵ���������
        public GameObject SourcePrefab { get; set; }    //�����ж�ʵ���������Ƿ�������ͬһ��Prefab
        public float CurrentCharge { get; private set; }
        public Vector3 MuzzleWorldVelocity { get; private set; }


        public bool TryShoot()
        {
            HandleShoot();
            return true;
        }
        void HandleShoot()
        {
            //�ӵ�ʵ����
            FlyingObjectBase newFlyingObject = Instantiate(FlyingObjectPrefab,WeaponMuzzle.position,WeaponMuzzle.rotation);
            newFlyingObject.Shoot(this);
        }
    }
}

