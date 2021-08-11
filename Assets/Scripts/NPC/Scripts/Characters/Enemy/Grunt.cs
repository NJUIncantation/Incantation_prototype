/*************************************************************************
 *  Copyright © 2021 DSH. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  NewBehaviourScript.cs
 *  Author       :  DSH
 *  Date         :  2021/8/11
 *************************************************************************/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Grunt : EnemyController
{
    [Header("Skill Settings")]
    public int kickForce = 20;
    public void KickOff()
    {
        if(attackTarget != null)
        {
            transform.LookAt(attackTarget.transform);
            Vector3 direction = attackTarget.transform.position - transform.position;
            direction.Normalize();

            attackTarget.GetComponent<NavMeshAgent>().isStopped = true;
            attackTarget.GetComponent<NavMeshAgent>().velocity = direction * kickForce;
        }
    }

}
