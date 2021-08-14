using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.NJUCS.Spell
{
    public class SpellLightning : VirtualSpell
    {
        public ParticleSystem particleSystem;
        public SpellLightning()
        {
            ManaCost = 10;
            CoolDown = 5;
        }
        private void OnDestroy()
        {
            Destroy(particleSystem);
        }
        public override bool Cast()
        {
            if(particleSystem == null)
                particleSystem = Instantiate(Resources.Load<ParticleSystem>("Spells/LightningPrison"));
            //������ȴ
            //Debug.Log("Cooldown: " + CoolDownTimeLeft());
            if (CoolDownTimeLeft() != 0)
                return false;

            if(particleSystem != null)
            {
                if (particleSystem.isPlaying)
                    return false;
                particleSystem.transform.position = Master.transform.position;
                //particleSystem.transform.Rotate(180, 0, 0);
                //���¼����ͷ�ʱ��
                LastTimeCast = Time.time;
                particleSystem.Play();
                return true;
            }
            return false;
        }

    }

}
