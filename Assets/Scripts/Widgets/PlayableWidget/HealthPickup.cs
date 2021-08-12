using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.NJUCS.Game;
using Unity.NJUCS.Character;


namespace Unity.NJUCS.Widget
{
    public class HealthPickup : Pickup
    {
        [Header("Parameters")]
        [Tooltip("Amount of health to heal on pickup")]
        public float HealAmount;

        protected override void OnPicked(PlayerCharacter player)
        {
            Health playerHealth = player.GetComponent<Health>();
            if (playerHealth)
            {
                Debug.Log("get healed");
                playerHealth.Heal(HealAmount,player.gameObject);
                Destroy(gameObject);
            }

        }
    }
    
}

