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
        [Tooltip("��ʼʱװ��������")]
        public List<WeaponController> StartingWeapons = new List<WeaponController>();

        [Header("References")]
        [Tooltip("���������ᱻ��ӵ�����")]
        public Transform WeaponParentSocket;

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
            WeaponController weaponInstance = Instantiate(weaponPrefab, WeaponParentSocket);
            weaponInstance.Owner = gameObject;
            return true;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}


