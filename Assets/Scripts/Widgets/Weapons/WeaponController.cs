using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.NJUCS.Game;

namespace Unity.NJUCS.Widget
{
    public enum WeaponShootType
    {
        SingleShot,
        SpreadShot,
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

        [Tooltip("武器攻击方向（存放人物主体的transform）")]
        public Transform WeaponOriention;

        [Header("Shoot Parameters")]
        [Tooltip("Q技能攻击类型")]
        public WeaponShootType Q_ShootType;

        [Tooltip("Q技能消耗")]
        public float Q_ManaCost = 0f;

        [Tooltip("E技能攻击类型")]
        public WeaponShootType E_ShootType;

        [Tooltip("E技能消耗")]
        public float E_ManaCost = 0f;

        [Tooltip("随机扩散的最大角度(0表示没有扩散)")]
        public float BulletSpreadAngle = 0f;

        [Tooltip("一次发射弹丸的数量")]
        public int BulletsPerShot = 5;

        [Tooltip("单发子弹预制体")]
        public FlyingObjectBase SingleFlyingObjectPrefab;

        [Tooltip("霰弹子弹预制体")]
        public FlyingObjectBase SpreadFlyingObjectPrefab;

        public GameObject Owner { get; set; }           //记录谁拥有这个武器
        public GameObject SourcePrefab { get; set; }    //用来判断实例的武器是否来自于同一个Prefab
        public float CurrentCharge { get; private set; }
        public Vector3 MuzzleWorldVelocity { get; private set; }

        bool m_WantsToShoot = false;


        public bool HandleShootInputsQ(bool inputDown, bool inputHeld, bool inputUp)
        {
            Mana mana = Owner.GetComponent<Mana>();
            if (!mana.HaveEnoughMana(Q_ManaCost))
                return false;
            m_WantsToShoot = inputDown || inputHeld;
            switch (Q_ShootType)
            {
                case WeaponShootType.SingleShot:
                    if (inputDown)
                    {
                        return TryShoot();
                    }
                    return false;
                case WeaponShootType.SpreadShot:
                    if (inputDown)
                    {
                        return TryShootSpread();
                    }
                    return false;
                case WeaponShootType.Charge:
                    return false;

                default:return false;
            }
        }

        public bool HandleShootInputsE(bool inputDown, bool inputHeld, bool inputUp)
        {
            Mana mana = Owner.GetComponent<Mana>();
            if (!mana.HaveEnoughMana(E_ManaCost))
                return false;
            m_WantsToShoot = inputDown || inputHeld;
            switch (E_ShootType)
            {
                case WeaponShootType.SingleShot:
                    if (inputDown)
                    {
                        return TryShoot();
                    }
                    return false;
                case WeaponShootType.SpreadShot:
                    if (inputDown)
                    {
                        return TryShootSpread();
                    }
                    return false;
                case WeaponShootType.Charge:
                    return false;

                default: return false;
            }
        }



        public bool TryShoot()
        {
            HandleShoot();
            return true;
        }

        void HandleShoot()
        {
            //子弹实例化
            FlyingObjectBase newFlyingObject = Instantiate(SingleFlyingObjectPrefab, WeaponMuzzle.position, WeaponOriention.rotation);
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
                Vector3 shotDirection = GetShotDirectionWithinSpread(WeaponOriention);
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

