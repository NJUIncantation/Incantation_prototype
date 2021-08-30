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
        [Tooltip("开始时装备的武器")]
        public List<WeaponController> StartingWeapons = new List<WeaponController>();

        [Header("References")]
        [Tooltip("所有武器会被添加到父级")]
        public Transform WeaponParentSocket;

        [Tooltip("控制发射朝向")]
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
                //选择空闲的武器槽位加入武器
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

        // Control is called once per frame in playerInput
        public void Control(bool Q_inputDown, bool Q_inputHeld, bool Q_inputUp, bool E_inputDown, bool E_inputHeld, bool E_inputUp)
        {
            WeaponController activeWeapon = GetActiveWeapon();
            if(activeWeapon != null)
            {
                bool hasUsedQ = activeWeapon.HandleShootInputsQ(Q_inputDown, Q_inputHeld, Q_inputUp);
                bool hasUsedE = activeWeapon.HandleShootInputsE(E_inputDown, E_inputHeld, E_inputUp);
                Mana mana = gameObject.GetComponent<Mana>();
                if (hasUsedQ)
                {
                    mana.SpendMana(activeWeapon.Q_ManaCost, gameObject);
                    Debug.Log(message: "Q cost:" + activeWeapon.Q_ManaCost);
                }
                if (hasUsedE)
                {
                    mana.SpendMana(activeWeapon.E_ManaCost, gameObject);
                    Debug.Log(message: "E cost:" + activeWeapon.E_ManaCost);
                }
            }
            
            /*if (CrossPlatformInputManager.GetKeyDown(KeyCode.Q))
            {
                ActivedWeaponShootSingle();
            }
            if (CrossPlatformInputManager.GetKeyDown(KeyCode.E))
            {
                ActivedWeaponShootSpread();
            }*/
        }

        //当前使用的武器单发开火
        public void ActivedWeaponShootSingle()
        {
            m_WeaponSlots[ActiveWeaponIndex].TryShoot();
        }

        //当前使用的武器散发开火
        public void ActivedWeaponShootSpread()
        {
            m_WeaponSlots[ActiveWeaponIndex].TryShootSpread();
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

        public WeaponController GetActiveWeapon()
        {
            return GetWeaponAtSlotIndex(ActiveWeaponIndex);
        }

        public WeaponController GetWeaponAtSlotIndex(int index)
        {
            // find the active weapon in our weapon slots based on our active weapon index
            if (index >= 0 &&
                index < m_WeaponSlots.Length)
            {
                return m_WeaponSlots[index];
            }

            // if we didn't find a valid active weapon in our weapon slots, return null
            return null;
        }
    }
}


