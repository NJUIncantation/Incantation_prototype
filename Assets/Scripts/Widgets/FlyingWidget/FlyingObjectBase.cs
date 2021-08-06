using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Unity.NJUCS.Widget
{
    public abstract class FlyingObjectBase : MonoBehaviour
    {
        
        public GameObject Owner { get; private set; }                   //��¼�ӵ��ķ�����
        public Vector3 InitialPosition { get; private set; }            //�ӵ�����ĳ�ʼ����
        public Vector3 InitialDirection { get; private set; }           //�ӵ�����ĳ�ʼ����
        public Vector3 InheritedMuzzleVelocity { get; private set; }    //ǹ���ٶ�
        //public float InitialCharge { get; private set; }              //��������

        public UnityAction OnShoot;                                     //�󶨶�����Ӧ

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

