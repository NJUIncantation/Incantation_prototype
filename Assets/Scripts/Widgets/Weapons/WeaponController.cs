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

        [Tooltip("�����������򣨴�����������transform��")]
        public Transform WeaponOriention;

        [Header("Shoot Parameters")]
        [Tooltip("Q���ܹ�������")]
        public WeaponShootType Q_ShootType;

        [Tooltip("Q��������")]
        public float Q_ManaCost = 0f;

        [Tooltip("E���ܹ�������")]
        public WeaponShootType E_ShootType;

        [Tooltip("E��������")]
        public float E_ManaCost = 0f;

        [Tooltip("�����ɢ�����Ƕ�(0��ʾû����ɢ)")]
        public float BulletSpreadAngle = 0f;

        [Tooltip("һ�η��䵯�������")]
        public int BulletsPerShot = 5;

        [Tooltip("�����ӵ�Ԥ����")]
        public FlyingObjectBase SingleFlyingObjectPrefab;

        [Tooltip("�����ӵ�Ԥ����")]
        public FlyingObjectBase SpreadFlyingObjectPrefab;

        public GameObject Owner { get; set; }           //��¼˭ӵ���������
        public GameObject SourcePrefab { get; set; }    //�����ж�ʵ���������Ƿ�������ͬһ��Prefab
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
            //�ӵ�ʵ����
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

