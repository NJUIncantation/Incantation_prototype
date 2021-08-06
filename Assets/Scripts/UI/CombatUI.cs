// Author:WangJunYao
// UI for combat, based on UIBase
// Now including Avatar, HealthBar and ManaBar of player
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Unity.NJUCS.UI
{
    public class CombatUI : UIBase
    {
        // Canvas combat;

        [SerializeField] StateBar PlayerHealthBar;
        [SerializeField] StateBar PlayerManaBar;
        [SerializeField] Avatar PlayerAvatar;

        // Start is called before the first frame update
        protected override void Start()
        {
            UIName = "CombatUI";
            base.Start();

            ///Need to initialize health, mana and name of player here
            // For Test
            // InitializePlayerBar(250, 100);
            // InitializePlayerAvatar("Player");
        }

        // Update is called once per frame
        void Update()
        {
            // For Test
            // HealthBar.MycurrentValue -= 1;
            // ManaBar.MycurrentValue -= 1;
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