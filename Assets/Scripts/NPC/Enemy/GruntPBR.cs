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
            //��ǰ��������
            Vector3 norVec = transform.rotation * Vector3.forward;
            //����˵ķ�������
            Vector3 temVec = attackTarget.transform.position - transform.position;
            //���������ļн�
            float angle = Mathf.Acos(Vector3.Dot(norVec.normalized, temVec.normalized)) * Mathf.Rad2Deg;
            //FIXME:
            if (TargetInSkillRange())
            {
                if (angle <= 60 * 0.5f)
                {
                    Debug.Log("�ڼ��ܹ�����Χ��");
                    return true;
                }
            }
            return false;
        }
    }
}
