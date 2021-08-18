// Author:WangJunYao
// UI for combat, based on UIBase
// Now including Avatar, HealthBar and ManaBar of player and Compass
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.NJUCS.Game;

namespace Unity.NJUCS.UI
{
    public class CombatUI : BasePanel
    {
        // Canvas combat;

        private Canvas combatCanvas;
        [SerializeField] private StateBar PlayerHealthBar;
        [SerializeField] private StateBar PlayerManaBar;
        [SerializeField] private Avatar PlayerAvatar;
        [SerializeField] private Compass myCompass;

        private ActorManager m_actorManager;
        private CameraManager m_cameraManager;
        private Health playerHealth;
        private Mana playerMana;
        private GameObject MainCamera;

        // Start is called before the first frame update
        protected void Start()
        {
            combatCanvas = GetComponent<Canvas>();

            ///Need to initialize health, mana and name of player here
            
            
            // For Test
            InitializePlayerAvatar("Player");
        }

        // Update is called once per frame
        void Update()
        {
            if (m_actorManager == null)
            {
                m_actorManager = FindObjectOfType<ActorManager>();
                playerHealth = m_actorManager.FindActorByName("mainCharacter").GetComponent<Health>();
                playerMana = m_actorManager.FindActorByName("mainCharacter").GetComponent<Mana>();
                InitializePlayerBar(playerHealth.MaxHealth, playerMana.MaxMana);
            }
            if (m_cameraManager == null)
            {
                m_cameraManager = FindObjectOfType<CameraManager>();
                return;
            }
            MainCamera = m_cameraManager.FindCameraByName("mainCamera");
            if(MainCamera != null && myCompass != null)
            {
                myCompass.changeCompass(MainCamera.GetComponent<Transform>().eulerAngles.y);
            }

            //Debug.Log(m_actorManager.FindActorByName("mainCharacter").GetComponent<Camera>().transform.eulerAngles.y);
            PlayerHealthBar.MycurrentValue = playerHealth.CurrentHealth;
            //Debug.Log(playerHealth.CurrentHealth);
            PlayerManaBar.MycurrentValue = playerMana.CurrentMana;
        }


        private void InitializePlayerBar(float MaxHealth, float MaxMana)
        {
            PlayerHealthBar.Initialize(MaxHealth, MaxHealth);
            PlayerManaBar.Initialize(MaxMana, MaxMana);
        }

        private void InitializePlayerAvatar(string AvatarName)
        {
            PlayerAvatar.LoadAvatar(AvatarName);
        }
    }
}