using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Unity.NJUCS.Character
{
    [RequireComponent(typeof(Animator))]
    public class CharacterIK : MonoBehaviour
    {
        protected Animator animator;
        public CharacterMovement characterMovement;
        public Vector3 footOffset;
        private float ikWeight;
        private float lerpSpeed;
        private bool enable;
        // Start is called before the first frame update
        void Start()
        {
            animator = GetComponent<Animator>();
            lerpSpeed = 100;
            footOffset = new Vector3(0, 0.2f, 0);
            enable = true;
        }

        // Update is called once per frame
        void Update()
        {
            if (characterMovement.Velocity.magnitude >0&&enable)
            {
                ikWeight = Mathf.Lerp(ikWeight, 1, Time.deltaTime*lerpSpeed);
            }
            else
            {
                ikWeight = Mathf.Lerp(ikWeight, 0, Time.deltaTime * lerpSpeed);
            }
        }
        void OnAnimatorIK()
        {
            //Debug.Log("OnAnimitorIK");
            Vector3 leftFootPosition = animator.GetBoneTransform(HumanBodyBones.LeftFoot).position;
            Vector3 rightFootPosition = animator.GetBoneTransform(HumanBodyBones.RightFoot).position;
            leftFootPosition = GetHitPoint(leftFootPosition + Vector3.up, leftFootPosition - Vector3.up*5) + footOffset;
            rightFootPosition = GetHitPoint(rightFootPosition + Vector3.up, rightFootPosition - Vector3.up*5) + footOffset;

            //transform.localPosition = new Vector3(0, -Mathf.Abs(leftFootPosition.y - rightFootPosition.y) / 2*ikWeight, 0);
            if(Mathf.Abs(leftFootPosition.y - rightFootPosition.y) / 2 > 0.1f)
            {
                enable = true;
            }
            else
            {
                enable = false;
            }
            animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, ikWeight);
            animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, ikWeight);
            animator.SetIKPosition(AvatarIKGoal.LeftFoot, leftFootPosition);
            animator.SetIKPosition(AvatarIKGoal.RightFoot, rightFootPosition);
        }
        private Vector3 GetHitPoint(Vector3 start, Vector3 end)
        {
            RaycastHit hit;
            if (Physics.Linecast(start, end, out hit))
            {
                return hit.point;
            }
            else
            {
                return end;
            }
        }
    }
}
