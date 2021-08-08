using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
namespace Unity.NJUCS.NPC 
{

    public enum EnemyStates 
    {
        GUARD,
        PATROL,
        CHASE,
        DEAD
    }

    [RequireComponent(typeof(NavMeshAgent))]

    public class EnemyController : MonoBehaviour
    {
        private EnemyStates enemy_states;
        private NavMeshAgent agent;
        private Animator anim;

        [Header("Basic Settings")]

        public float sightRadius;  
        private float speed;
        public float lookAtTime;
        public bool isGuard;
        private float remainLookAtTime; 
        private GameObject attackTarget;
        private Quaternion guardRotation;

        [Header("Patrol State")]

        public float patrolRange;

        private Vector3 wayPoint;//随机巡逻点  
        private Vector3 guardPos;//初始位置

        //配合动画的bool变量
        bool isWalk;
        bool isChase;
        bool isFollow;

        void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            anim = GetComponent<Animator>();
            speed = agent.speed;
            guardPos = transform.position;
            guardRotation = transform.rotation;
            remainLookAtTime = lookAtTime;
        }
        void Start() //与Awake不同，在点击play后才执行
        {
            if(isGuard)
            {
                enemy_states = EnemyStates.GUARD;
            }
            else
            {
                enemy_states = EnemyStates.PATROL;
                GetNewWayPoint();//得到初始移动的点
            }
        }
        void Update()
        {       
            SwitchStates();
            SwitchAnimation();
        }

        void SwitchAnimation()
        {
            anim.SetBool("Walk",isWalk);
            anim.SetBool("Chase",isChase);
            anim.SetBool("Follow",isFollow);
        }

        void SwitchStates()
        {
    
            if(FoundPlayer())
            {
                enemy_states = EnemyStates.CHASE;
                //Debug.Log("Enemy have found player!");
            }

            switch(enemy_states)
            {
                case EnemyStates.GUARD:
                    isChase = false;
                    if(transform.position != guardPos)
                    {
                        isWalk = true;
                        agent.isStopped = false;
                        agent.destination = guardPos;
                    
                        if(Vector3.SqrMagnitude(guardPos-transform.position) <= agent.stoppingDistance
                            && transform.rotation != guardRotation)
                        {
                            isWalk = false;
                            //Debug.Log("Enemy is rotating!");
                            transform.rotation = Quaternion.Lerp(transform.rotation, guardRotation, 0.03f);
                        }
                    }
                    break;
                case EnemyStates.PATROL:

                    isChase = false;
                    agent.speed = speed * 0.5f;
               
                    if(Vector3.Distance(wayPoint, transform.position) <= agent.stoppingDistance)
                    {
                        isWalk = false;
                        //到了巡逻点应该停下来等待一段时间
                        if(remainLookAtTime > 0)
                        {
                            remainLookAtTime -= Time.deltaTime;
                        }
                        else
                        {                       
                            GetNewWayPoint();
                        }
                    }
                    else
                    {
                        isWalk = true;
                        agent.destination = wayPoint;
                    }
                    break;
                case EnemyStates.CHASE:                             
                
                    isWalk = false;
                    isChase = true;

                    agent.speed = speed;
                    if(FoundPlayer())
                    {
                        // 追击player
                        isFollow = true;
                        agent.destination = attackTarget.transform.position;
                        //TODO: 进入攻击范围切换状态
                    }
                    else
                    {
                        //拉脱距离切换状态
                        isFollow = false;
                        if(remainLookAtTime > 0)
                        {
                            agent.destination = transform.position;//停住观察
                            remainLookAtTime -= Time.deltaTime;
                        }
                        else if(isGuard)
                        {
                            enemy_states = EnemyStates.GUARD;
                        }
                        else
                        {                       
                            enemy_states = EnemyStates.PATROL;
                        }                  
                    }
                    break;
                case EnemyStates.DEAD:
                    break;
                default:
                    Debug.Log("error in function SwitchStates of class EnemyController!\n");
                    enemy_states = EnemyStates.DEAD;
                    break;
            }
        }

        bool FoundPlayer()
        {
        
            var colliders = Physics.OverlapSphere(transform.position, sightRadius);

            foreach (var target in colliders)
            {
                if(target.CompareTag("Player"))
                {
                    attackTarget = target.gameObject;
                    return true;
                }
            } 

            attackTarget = null;
            return false;
        }

        void GetNewWayPoint()
        {
            remainLookAtTime = lookAtTime;

            float randomX = Random.Range(-patrolRange, patrolRange);
            float randomZ = Random.Range(-patrolRange, patrolRange);

            Vector3 randomPoint = new Vector3(guardPos.x+randomX,transform.position.y,guardPos.z+randomZ);
            NavMeshHit hit;
            wayPoint = NavMesh.SamplePosition(randomPoint,out hit,patrolRange,1)?hit.position:transform.position;
     
        }

        private void OnDrawGizmosSelected() {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, sightRadius);
        }

    }
}

