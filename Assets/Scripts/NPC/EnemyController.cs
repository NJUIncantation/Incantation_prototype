using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Unity.NJUCS.Game;
using Unity.NJUCS.UI;

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
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(Damageable))]
    [RequireComponent(typeof(BoxCollider))]
    [RequireComponent(typeof(StateBar))]
    [RequireComponent(typeof(AttackStats))]
    public class EnemyController : VirtualEnemy
    {
        private EnemyStates enemyStates;
        private NavMeshAgent agent;
        private Animator anim;
        private Collider coll;
        private Health health;
        private AttackStats attackStats;
        [SerializeField] private StateBar enemyHealthBar;
        private CameraManager m_cameraManager;
        private GameObject MainCamera;

        [Header("Basic Settings")]
        public float sightRadius;
        private float speed;
        public float lookAtTime;
        public bool isGuard;
        private float remainLookAtTime;
        private float lastAttackTime;
        protected GameObject attackTarget;
        private Quaternion guardRotation;

        [Header("Patrol State")]
        public float patrolRange;
        private Vector3 wayPoint;//���Ѳ�ߵ�
        private Vector3 guardPos;//��ʼλ��

        //[Header("Test variable")]
        //public float coolDown;
        //public float attackRange;
        //public float skillRange;


        //��϶�����bool����
        bool isWalk = false;
        bool isChase = false;
        bool isFollow = false;
        bool isDead = false;
        //bool playerDead = false;
        bool isCritical = false;

        public void OnCameraCreatedFunc(string name, GameObject gameobject)
        { 
            MainCamera = m_cameraManager.FindCameraByName("mainCamera");
            Debug.Log("get camera");
        }

        void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            anim = GetComponent<Animator>();
            coll = GetComponent<Collider>();
            health = GetComponent<Health>();
            attackStats = GetComponent<AttackStats>();
            speed = agent.speed;
            guardPos = transform.position;
            guardRotation = transform.rotation;
            remainLookAtTime = lookAtTime;

            m_cameraManager = FindObjectOfType<CameraManager>();
            m_cameraManager.OnCameraCreated += OnCameraCreatedFunc;
            if (m_cameraManager.FindCameraByName("mainCamera") != null)
            {
                MainCamera = m_cameraManager.FindCameraByName("mainCamera");
                Debug.Log("get camera");
            }
        }
        void Start() //��Awake��ͬ���ڵ��play���ִ��
        {
            //FIXME:
            EnemyManager.Instance.AddEnemy(this);
            if (health != null)
            {
                health.OnDie += OnDie;
                enemyHealthBar.Initialize(health.MaxHealth, health.MaxHealth);
            }
            if (isGuard)
            {
                enemyStates = EnemyStates.GUARD;
            }
            else
            {
                enemyStates = EnemyStates.PATROL;
                GetNewWayPoint();//�õ���ʼ�ƶ��ĵ�
            }
        }

        //void OnEnable()
        //{
        //    GameManager.Instance.AddObserver(this);
        //}

        void OnDisable()
        {
            if (EnemyManager.IsInitialized)
            {
                //FIXME:
                EnemyManager.Instance.RemoveEnemy(this);
            }
        }

        void OnDie()
        {
            //Debug.Log("broadcasting");
            EventManager.Broadcast(Events.EnemyKillEvent);
        }

        void Update()
        {
            if (health.CurrentHealth <= 0)
            {
                isDead = true;
            }
            /*if (!playerDead)
            {
                SwitchStates();
                SwitchAnimation();
                lastAttackTime -= Time.deltaTime;
            }*/
            SwitchStates();
            SwitchAnimation();
            lastAttackTime -= Time.deltaTime;
            updateHealthBar();
        }

        void SwitchAnimation()
        {
            anim.SetBool("Walk", isWalk);
            anim.SetBool("Chase", isChase);
            anim.SetBool("Follow", isFollow);
            anim.SetBool("Critical", attackStats.isCritical);
            anim.SetBool("Death", isDead);
        }

        void SwitchStates()
        {
            if (isDead)
            {
                enemyStates = EnemyStates.DEAD;
            }
            else if (FoundPlayer())
            {
                enemyStates = EnemyStates.CHASE;
                //Debug.Log("Enemy have found player!");
            }

            switch (enemyStates)
            {
                case EnemyStates.GUARD:
                    isChase = false;
                    if (transform.position != guardPos)
                    {
                        //print("guardPos" + guardPos);
                        //print("position" + transform.position);
                        //print(transform.position - guardPos);
                        isWalk = true;
                        agent.isStopped = false;
                        agent.destination = guardPos;

                        if (Vector3.SqrMagnitude(guardPos - transform.position) <= agent.stoppingDistance
                            && transform.rotation != guardRotation)
                        {
                            isWalk = false;
                            //Debug.Log("Enemy is rotating!");
                            transform.rotation = Quaternion.Lerp(transform.rotation, guardRotation, 0.01f);
                        }
                    }
                    break;
                case EnemyStates.PATROL:                   
                    isChase = false;
                    agent.speed = speed * 0.5f;

                    if (Vector3.Distance(wayPoint, transform.position) <= agent.stoppingDistance)
                    {
                        isWalk = false;
                        //����Ѳ�ߵ�Ӧ��ͣ�����ȴ�һ��ʱ��
                        if (remainLookAtTime > 0)
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
                        agent.isStopped = false;
                        agent.destination = wayPoint;
                    }
                    break;
                case EnemyStates.CHASE:

                    isWalk = false;
                    isChase = true;
                    agent.speed = speed;

                    if (FoundPlayer())
                    {
                        // ׷��player
                        //Debug.Log("Found Enemy!");
                        isFollow = true;
                        agent.isStopped = false;
                        agent.destination = attackTarget.transform.position;
                    }
                    else
                    {
                        //���Ѿ����л�״̬
                        isFollow = false;
                        if (remainLookAtTime > 0)
                        {
                            agent.destination = transform.position;//ͣס�۲�
                            remainLookAtTime -= Time.deltaTime;
                        }
                        else if (isGuard)
                        {
                            enemyStates = EnemyStates.GUARD;
                        }
                        else
                        {
                            enemyStates = EnemyStates.PATROL;
                        }
                    }
                    //���빥����Χ�л�״̬
                    if (TargetInAttackRange() || TargetInSkillRange())
                    {
                        isFollow = false;
                        agent.isStopped = true;
                        if (lastAttackTime < 0)
                        {
                            lastAttackTime = attackStats.CoolDown;
                            //�����ж�
                            attackStats.isCritical = Random.value < attackStats.CriticalChance;
                            Attack();
                        }
                    }
                    break;
                case EnemyStates.DEAD:
                    coll.enabled = false;
                    agent.radius = 0;
                    DestroyEnemy();
                    break;
                default:
                    Debug.Log("error in function SwitchStates of class EnemyController!\n");
                    enemyStates = EnemyStates.DEAD;
                    break;
            }
        }

        private void DestroyEnemy()
        {
            EnemyManager.Instance.RemoveEnemy(this);
            Destroy(gameObject, 2f);
        }

        bool TargetInAttackRange()
        {
            if (attackTarget != null)
            {
                return Vector3.Distance(attackTarget.transform.position, transform.position) 
                    <= attackStats.ATKRange + attackTarget.GetComponent<Collider>().bounds.size.x/2.0f;
            }
            else return false;
        }

        protected bool TargetInSkillRange()
        {
            if (attackTarget != null)
            {
                return Vector3.Distance(attackTarget.transform.position, transform.position) 
                    <= attackStats.SkillRange + attackTarget.GetComponent<Collider>().bounds.size.x/2.0f;
            }
            else return false;
        }

        void Attack()
        {
            transform.LookAt(attackTarget.transform);
            if (TargetInAttackRange())//��������
            {
                anim.SetTrigger("Attack");
            }
            if (TargetInSkillRange())//���ܶ���
            {
                //print("set the trigger Skill!");
                anim.SetTrigger("Skill");
            }
        }

        void Hit()
        {
            //��Ϊanimation event������
            //�жϷ�Χ
            if(HitCheck() == true)
            {
                //�˺�����
                float baseDamage = Random.Range(attackStats.MinATK, attackStats.MaxATK);
                float finalDamage = attackStats.isCritical ? 
                    baseDamage * attackStats.CriticalMultiplier : baseDamage;
                //Debug.Log("�����˺�:"+finalDamage);
                attackTarget.GetComponent<Health>().TakeDamage(finalDamage, anim.GetComponent<GameObject>());
            }
        }

        bool HitCheck()
        {
            if(attackTarget == null)
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
            if (TargetInAttackRange())
            {
                if (angle <= 60 * 0.5f)
                {
                    //Debug.Log("�����ι�����Χ��");
                    return true;
                }
            }
            return false;
        }

        bool FoundPlayer()
        {

            var colliders = Physics.OverlapSphere(transform.position, sightRadius);

            foreach (var target in colliders)
            {
                if (target.CompareTag("Player"))
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

            Vector3 randomPoint = new Vector3(guardPos.x + randomX, transform.position.y, guardPos.z + randomZ);
            NavMeshHit hit;

            wayPoint = NavMesh.SamplePosition(randomPoint, out hit, patrolRange, 1) ? hit.position : transform.position;
            //Debug.Log("position: " + wayPoint);

        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, sightRadius);
        }


        //public void EndNotify()
        //{
        //    //��Ϸֹͣ
        //    playerDead = true;
        //    isChase = false;
        //    isWalk = false;
        //    attackTarget = null;
        //    anim.SetBool("Win", true);
        //}
        void updateHealthBar()
        {
            enemyHealthBar.MycurrentValue = health.CurrentHealth;
            enemyHealthBar.ChangeAngle("Enemy", MainCamera);
            if(MainCamera == null)
            {
                Debug.Log("no camera");
            }
        }
    }
}
