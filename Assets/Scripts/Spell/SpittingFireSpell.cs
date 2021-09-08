using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.NJUCS.Game;

namespace Unity.NJUCS.Spell
{
    public class SpittingFireSpell : VirtualSpell
    {
        public ParticleSystem particleSystem;
        private DamageArea damageArea;

        public SpittingFireSpell()
        {
            ManaCost = 20;
            CoolDown = 8;
            particleSystem = Instantiate(Resources.Load<ParticleSystem>("Spells/SpittingFire"));
            damageArea = particleSystem.GetComponent<DamageArea>();
            damageArea.AreaOfEffectDistance = 15;
        }
        private void OnDestroy()
        {
            Destroy(particleSystem);
        }

        public override bool Cast()
        {
            if (CoolDownTimeLeft() != 0)
                return false;

            if (particleSystem != null)
            {
                //本技能释放中心为当前角色位置
                particleSystem.transform.position = Master.transform.position + Master.transform.localScale.magnitude * 0.8f * Vector3.up;

                //创建伤害区域
                damageArea.Owner = Master;
                Debug.Log(Master.transform.localScale.magnitude);
                damageArea.Center = Master.transform.position + Master.transform.localScale.magnitude * Vector3.up * 0.8f;
                damageArea.Damage = 20;
                damageArea.InflictDamageInArea(
                     LayerMask.NameToLayer("AllLayer"),
                     QueryTriggerInteraction.UseGlobal,
                     DamageDecayByDistance
                    );
                if (particleSystem.isPlaying)
                    return false;

                //particleSystem.transform.Rotate(180, 0, 0);
                //更新技能释放时间
                LastTimeCast = Time.time;
                particleSystem.Play();

                
                return true;
            }
            return false;
        }

        private float DamageDecayByDistance(Vector3 currentPosition)
        {
            Vector3 temp = damageArea.Center - currentPosition;
            float angle =  Vector3.Angle(temp, Master.transform.forward);
            float maxVectorLength = (particleSystem.main.duration * particleSystem.main.startSpeed.constant) / Mathf.Cos(particleSystem.shape.angle);
            if (particleSystem.shape.angle - angle > 0 && temp.magnitude < maxVectorLength)
            {
                return damageArea.Damage * (particleSystem.shape.angle - angle);
            }
            else return 0.0f;
        }
    }

}
