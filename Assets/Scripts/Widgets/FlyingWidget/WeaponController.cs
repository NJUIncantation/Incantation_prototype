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
        [Tooltip("该武器名字会在UI中显示")]
        public string WeaponName;

        [Header("Internal References")]
        //[Tooltip("The root object for the weapon, this is what will be deactivated when the weapon isn't active")]
        //public GameObject WeaponRoot;

        [Tooltip("存放枪口的位置")]
        public Transform WeaponMuzzle;

        [Header("Shoot Parameters")]
        [Tooltip("The type of weapon wil affect how it shoots")]
        public WeaponShootType ShootType;

        [Tooltip("子弹预制体")]
        public FlyingObjectBase FlyingObjectPrefab;

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
            FlyingObjectBase newFlyingObject = Instantiate(FlyingObjectPrefab,WeaponMuzzle.position,WeaponMuzzle.rotation);
            newFlyingObject.Shoot(this);
        }
    }
}

