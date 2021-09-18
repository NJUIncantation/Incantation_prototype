using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.NJUCS.Tools
{
    public static class ExtensionMethod
    {
        private const float dotThreshold = 0.5f;
        public static bool TargetInFanShapedRange(this Transform trans, Transform target)
        {
            if(target != null)
            {
                var vectorToTarget =  target.position - trans.position;
                vectorToTarget.Normalize();

                float dot = Vector3.Dot(trans.forward, vectorToTarget);//º∆À„º–Ω«”‡œ“÷µ
                return dot <= dotThreshold;
            }
            return false;
        }
    }
}

