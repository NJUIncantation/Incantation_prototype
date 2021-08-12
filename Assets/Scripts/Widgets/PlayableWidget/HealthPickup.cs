using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Unity.NJUCS.Widget
{
    public class HealthPickup : Pickup
    {
        [Header("Parameters")]
        [Tooltip("Amount of health to heal on pickup")]
        public float HealAmount;
    }
}

