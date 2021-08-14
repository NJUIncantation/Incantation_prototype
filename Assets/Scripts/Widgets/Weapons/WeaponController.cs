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
        //[Tooltip("开始时拥有的技能")]
        //public List<WeaponShootType> StartingShootType = new List<WeaponShootType>();

        [Header("Information")]
        [Tooltip("该武器名字会在UI中显示")]
        public string WeaponName;

        [Header("Internal References")]
        //[Tooltip("The root object for the weapon, this is what will be deactivated when the weapon isn't active")]
        //public GameObject WeaponRoot;
        [Tooltip("存放枪口的位置")]
        public Transform WeaponMuzzle;

        [Header("Shoot Parameters")]
        [Tooltip("武器攻击类型")]
        public WeaponShootType ShootType;

        [Tooltip("Angle for the cone in which the bullets will be shot randomly (0 means no spread at all)")]
        public float BulletSpreadAngle = 0f;

        [Tooltip("Amount of bullets per shot")]
        public int BulletsPerShot = 5;

        [Tooltip("单发子弹预制体")]
        public FlyingObjectBase SingleFlyingObjectPrefab;

        [Tooltip("霰弹子弹预制体")]
        public FlyingObjectBase SpreadFlyingObjectPrefab;

        public GameObject Owner { get; set; }           //记录谁拥有这个武器
        public GameObject SourcePrefab { get; set; }    //用来判断实例的武器是否来自于同一个Prefab
        public float CurrentCharge { get; private set; }
        public Vector3 MuzzleWorldVelocity { get; private set; }


        public bool TryShoot()
        {
            HandleShoot();
            return true;
        }

        void HandleShoot()
        {
            //子弹实例化
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

