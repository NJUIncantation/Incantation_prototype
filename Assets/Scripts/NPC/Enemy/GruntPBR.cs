using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.NJUCS.NPC
{
    public class GruntPBR : EnemyController
    {
        [Header("Skill")]
        public float kickForce = 10f;

        void KickOff()
        {
            if(SkillCheck())
            {
                transform.LookAt(attackTarget.transform);
                Vector3 direction = attackTarget.transform.position - transform.position;
                direction.Normalize();

                attackTarget.GetComponent<Rigidbody>().AddForce(direction * kickForce, ForceMode.Impulse);
            }
        }
        bool SkillCheck()
        {
            if (attackTarget == null)
            {
                return false;
            }
            //正前方的向量
            Vector3 norVec = transform.rotation * Vector3.forward;
            //与敌人的方向向量
            Vector3 temVec = attackTarget.transform.position - transform.position;
            //两个向量的夹角
            float angle = Mathf.Acos(Vector3.Dot(norVec.normalized, temVec.normalized)) * Mathf.Rad2Deg;
            //FIXME:
            if (TargetInSkillRange())
            {
                if (angle <= 60 * 0.5f)
                {
                    Debug.Log("在技能攻击范围内");
                    return true;
                }
            }
            return false;
        }
    }
}
