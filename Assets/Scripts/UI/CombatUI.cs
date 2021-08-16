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
    public class CombatUI : MonoBehaviour
    {
        // Canvas combat;

        private Canvas combatCanvas;
        [SerializeField] private StateBar PlayerHealthBar;
        [SerializeField] private StateBar PlayerManaBar;
        [SerializeField] private Avatar PlayerAvatar;
        [SerializeField] private Compass myCompass;

        private ActorManager m_actorManager;
        private Health health;

        private CameraManager m_cameraManager;

        // Start is called before the first frame update
        protected void Start()
        {
            combatCanvas = GetComponent<Canvas>();

            ///Need to initialize health, mana and name of player here

            m_actorManager = FindObjectOfType<ActorManager>();
            health = m_actorManager.FindActorByName("mainCharacter").GetComponent<Health>();
            InitializePlayerBar(health.MaxHealth, 0);

            m_cameraManager = FindObjectOfType<CameraManager>();
            // For Test
            //InitializePlayerAvatar("Player");
        }

        // Update is called once per frame
        void Update()
        {
            if (m_cameraManager.FindCameraByName("mainCamera").GetComponent<Transform>().eulerAngles == null)
                Debug.LogError("cao");
            //Debug.Log(m_actorManager.FindActorByName("mainCharacter").GetComponent<Transform>().transform.eulerAngles.y);
            // PlayerHealthBar.MycurrentValue = health.CurrentHealth;

            // For Test
            // PlayerManaBar.MycurrentValue -= 1;
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