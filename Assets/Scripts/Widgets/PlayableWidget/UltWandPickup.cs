using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.NJUCS.Game;
using Unity.NJUCS.Character;


namespace Unity.NJUCS.Widget
{
    public class UltWandPickup : Pickup
    {
        [Header("Parameters")]
        [Tooltip("Weapon prefab")]
        public WeaponController weapon;

        protected override void OnPicked(PlayerCharacter player)
        {
            /*Health playerHealth = player.GetComponent<Health>();
            if (playerHealth)
            {
                Debug.Log("get healed");
                playerHealth.Heal(HealAmount, player.gameObject);
                Destroy(gameObject);
            }*/
            Destroy(gameObject);
        }
    }

}
