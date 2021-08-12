using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Unity.NJUCS.Widget;

namespace Unity.NJUCS.Game
{
    [RequireComponent(typeof(Unity.NJUCS.PlayerInput.CrossPlatformInputManager))]
    public class PlayerWeaponManager : MonoBehaviour
    {
        [Tooltip("开始时装备的武器")]
        public List<WeaponController> StartingWeapons = new List<WeaponController>();

        [Header("References")]
        [Tooltip("所有武器会被添加到父级")]
        public Transform WeaponParentSocket;

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
                //选择空闲的武器槽位加入武器
                if (m_WeaponSlots[i] == null)
                {
                    WeaponController weaponInstance = Instantiate(weaponPrefab, WeaponParentSocket);
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

        }

        //当前使用的武器开火
        public void ActivedWeaponShoot()
        {
            m_WeaponSlots[ActiveWeaponIndex].TryShoot();
        }

        //避免重复添加武器
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


