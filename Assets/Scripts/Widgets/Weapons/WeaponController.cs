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
        //[Tooltip("��ʼʱӵ�еļ���")]
        //public List<WeaponShootType> StartingShootType = new List<WeaponShootType>();

        [Header("Information")]
        [Tooltip("���������ֻ���UI����ʾ")]
        public string WeaponName;

        [Header("Internal References")]
        //[Tooltip("The root object for the weapon, this is what will be deactivated when the weapon isn't active")]
        //public GameObject WeaponRoot;
        [Tooltip("���ǹ�ڵ�λ��")]
        public Transform WeaponMuzzle;

        [Header("Shoot Parameters")]
        [Tooltip("������������")]
        public WeaponShootType ShootType;

        [Tooltip("Angle for the cone in which the bullets will be shot randomly (0 means no spread at all)")]
        public float BulletSpreadAngle = 0f;

        [Tooltip("Amount of bullets per shot")]
        public int BulletsPerShot = 5;

        [Tooltip("�����ӵ�Ԥ����")]
        public FlyingObjectBase SingleFlyingObjectPrefab;

        [Tooltip("�����ӵ�Ԥ����")]
        public FlyingObjectBase SpreadFlyingObjectPrefab;

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
            FlyingObjectBase newFlyingObject = Instantiate(SingleFlyingObjectPrefab,WeaponMuzzle.position,WeaponMuzzle.rotation);
            newFlyingObject.Shoot(this);
        }

        public bool TryShootSpread()
        {
            HandleShootSpread();
            return true;
        }

        void HandleShootSpread()
        {
            for(int i = 0; i < BulletsPerShot; i++)
            {
                Vector3 shotDirection = GetShotDirectionWithinSpread(WeaponMuzzle);
                FlyingObjectBase newFlyingObject = Instantiate(SpreadFlyingObjectPrefab, WeaponMuzzle.position, 
                    Quaternion.LookRotation(shotDirection));
                newFlyingObject.Shoot(this);
            }
        }

        public Vector3 GetShotDirectionWithinSpread(Transform shootTransform)
        {
            float spreadAngleRatio = BulletSpreadAngle / 180f;
            Vector3 spreadWorldDirection = Vector3.Slerp(shootTransform.forward, UnityEngine.Random.insideUnitSphere,
                spreadAngleRatio);

            return spreadWorldDirection;
        }


    }
}

