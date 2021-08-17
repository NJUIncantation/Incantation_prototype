using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Unity.NJUCS.Widget;
using Unity.NJUCS.PlayerInput;

namespace Unity.NJUCS.Game
{
    [RequireComponent(typeof(Unity.NJUCS.PlayerInput.CrossPlatformInputManager))]
    public class PlayerWeaponManager : MonoBehaviour
    {
        [Tooltip("��ʼʱװ��������")]
        public List<WeaponController> StartingWeapons = new List<WeaponController>();

        [Header("References")]
        [Tooltip("���������ᱻ��ӵ�����")]
        public Transform WeaponParentSocket;

        [Tooltip("���Ʒ��䳯��")]
        public Transform WeaponOriention;

        public int ActiveWeaponIndex { get; private set; }

        WeaponController[] m_WeaponSlots = new WeaponController[9]; // 9 available weapon slots
        


        // Start is called before the first frame update
        void Start()
        {
            // Add starting weapons
            foreach (var weapon in StartingWeapons)
            {
                AddWeapon(weapon);
            }
        }

        public bool AddWeapon(WeaponController weaponPrefab)
        {
            if (HasWeapon(weaponPrefab) != null)
            {
                return false;
            }
            for (int i = 0; i < m_WeaponSlots.Length; i++)
            {
                //ѡ����е�������λ��������
                if (m_WeaponSlots[i] == null)
                {
                    WeaponController weaponInstance = Instantiate(weaponPrefab, WeaponParentSocket);
                    weaponInstance.WeaponOriention = WeaponOriention;
                    weaponInstance.transform.localPosition = Vector3.zero;
                    weaponInstance.transform.localRotation = Quaternion.identity;

                    weaponInstance.Owner = gameObject;
                    weaponInstance.SourcePrefab = weaponPrefab.gameObject;
                    m_WeaponSlots[i] = weaponInstance;
                    return true;
                }
            }


            return false ;
        }

        // Update is called once per frame
        void Update()
        {
            if (CrossPlatformInputManager.GetKeyDown(KeyCode.Q))
            {
                ActivedWeaponShootSingle();
            }
            if (CrossPlatformInputManager.GetKeyDown(KeyCode.E))
            {
                ActivedWeaponShootSpread();
            }
        }

        //��ǰʹ�õ�������������
        public void ActivedWeaponShootSingle()
        {
            m_WeaponSlots[ActiveWeaponIndex].TryShoot();
        }

        //��ǰʹ�õ�����ɢ������
        public void ActivedWeaponShootSpread()
        {
            m_WeaponSlots[ActiveWeaponIndex].TryShootSpread();
        }

        //�����ظ��������
        public WeaponController HasWeapon(WeaponController weaponPrefab)
        {
            for (var index = 0; index < m_WeaponSlots.Length; index++)
            {
                var w = m_WeaponSlots[index];
                if (w != null && w.SourcePrefab == weaponPrefab.gameObject)
                {
                    return w;
                }
            }

            return null;
        }
    }
}


