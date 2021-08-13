using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.NJUCS.Spell
{
    public class SpellLightning : VirtualSpell
    {
        public ParticleSystem particleSystem;
        public override void Cast()
        {
            particleSystem = Resources.Load<ParticleSystem>("Spells/LightningPrison");
            if(particleSystem != null)
            {
                Debug.Log("Casting Lightning");
                particleSystem.Emit(10);
                particleSystem.Play();
                Debug.Log(particleSystem.IsAlive());
                return;
            }
            Debug.Log("Lightning Particle Effect Not Found");
        }
    }

}
