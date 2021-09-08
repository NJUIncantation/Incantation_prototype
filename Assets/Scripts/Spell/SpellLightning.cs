using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.NJUCS.Game;

namespace Unity.NJUCS.Spell
{
    public class SpellLightning : VirtualSpell
    {
        public ParticleSystem particleSystem;
        private DamageArea damageArea;
        public SpellLightning()
        {
            ManaCost = 10;
            CoolDown = 5;
            damageArea = new DamageArea();
            damageArea.AreaOfEffectDistance = 4f;
            particleSystem = Instantiate(Resources.Load<ParticleSystem>("Spells/LightningPrison"));
        }
        private void OnDestroy()
        {
            Destroy(particleSystem);
        }
        public override bool Cast()
        {
            //仍在冷却
            //Debug.Log("Cooldown: " + CoolDownTimeLeft());
            if (CoolDownTimeLeft() != 0)
                return false;

            if(particleSystem != null)
            {
                if (particleSystem.isPlaying)
                    return false;
                //本技能释放中心为当前角色位置
                particleSystem.transform.position = Master.transform.position;

                //particleSystem.transform.Rotate(180, 0, 0);
                //更新技能释放时间
                LastTimeCast = Time.time;
                particleSystem.Play();

                //创建伤害区域
                damageArea.Owner = Master;
                damageArea.Center = Master.transform.position;
                damageArea.Damage = 10;
                damageArea.InflictDamageInArea(
                     LayerMask.NameToLayer("AllLayer"),
                     QueryTriggerInteraction.UseGlobal
                    );
                return true;
            }
            return false;
        }

    }

}
