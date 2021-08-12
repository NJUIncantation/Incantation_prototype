// Author: 刘永鹏
// 修改时间: 2021.8.12
// 创建球形持续伤害区域


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.NJUCS.Game
{
    public class FireDamageArea : MonoBehaviour
    {
        private float m_LastTime;
        private float m_DamageTimeGap = 1.0f;
        DamageArea damageArea;
        
        // Start is called before the first frame update
        void Start()
        {
            m_LastTime = Time.time;
            damageArea = GetComponent<DamageArea>();
            if(damageArea == null)
            {
                Debug.LogError("No DamageArea Found");
            }
            damageArea.Center = transform.position;
            damageArea.AreaOfEffectDistance = 1.0f;
            damageArea.Owner = gameObject;
            damageArea.Damage = 10;
        }

        // Update is called once per frame
        void Update()
        {
            Debug.Log("In Fire");
            float currentTime = Time.time;
            if(currentTime - m_LastTime >= m_DamageTimeGap)
            {
                Debug.Log("Trigger FireDamageArea");
                damageArea.InflictDamageInArea(
                    LayerMask.NameToLayer("AllLayer"), 
                    QueryTriggerInteraction.UseGlobal,
                    (Vector3 currentPostion) => {
                        float damage = damageArea.Damage * ((1 - (currentPostion - damageArea.Center).magnitude) / damageArea.AreaOfEffectDistance);
                        return damage > 0 ? damage : 0;
                    }
                    );
                m_LastTime = currentTime;
            }

        }
    }
}

