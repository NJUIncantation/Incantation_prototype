using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Unity.NJUCS.Widget
{
    public abstract class FlyingObjectBase : MonoBehaviour
    {
        
        public GameObject Owner { get; private set; }                   //记录子弹的发射者
        public Vector3 InitialPosition { get; private set; }            //子弹本身的初始坐标
        public Vector3 InitialDirection { get; private set; }           //子弹本身的初始朝向
        public Vector3 InheritedMuzzleVelocity { get; private set; }    //枪口速度
        //public float InitialCharge { get; private set; }              //充能消耗

        public UnityAction OnShoot;                                     //绑定动作响应

        public void Shoot(WeaponController controller)
        {
            Owner = controller.Owner;
            InitialPosition = transform.position;
            InitialDirection = transform.forward;
            InheritedMuzzleVelocity = controller.MuzzleWorldVelocity;
            //InitialCharge = controller.CurrentCharge;

            OnShoot?.Invoke();
        }
        // Start is called before the first frame update
        /*void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }*/
    }
}

