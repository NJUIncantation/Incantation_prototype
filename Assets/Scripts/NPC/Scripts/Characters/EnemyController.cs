using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyStates 
{
    GUARD,
    PATROL,
    CHASE,
    DEAD
}
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(CharacterStats))]

public class EnemyController : MonoBehaviour, IEndGameOberver
{
    private EnemyStates enemy_states;
    private NavMeshAgent agent;
    private Animator anim;
    private CharacterStats characterStats;
    private Collider coll;

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

    private Vector3 wayPoint;//随机巡逻点
    
    private Vector3 guardPos;//初始位置

    //配合动画的bool变量
    bool isWalk = false;
    bool isChase = false;
    bool isFollow = false;
    bool isDead = false;
    bool playerDead = false;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        characterStats = GetComponent<CharacterStats>();
        coll = GetComponent<Collider>();
        speed = agent.speed;
        guardPos = transform.position;
        guardRotation = transform.rotation;
        remainLookAtTime = lookAtTime;
    }
    void Start() //与Awake不同，在点击play后才执行
    {
        //FIXME:
        GameManager.Instance.AddObserver(this);
        if (isGuard)
        {
            enemy_states = EnemyStates.GUARD;
        }
        else
        {
            enemy_states = EnemyStates.PATROL;
            GetNewWayPoint();//得到初始移动的点
        }
    }

    //void OnEnable()
    //{
    //    GameManager.Instance.AddObserver(this);
    //}

    void OnDisable()
    {
        if(GameManager.IsInitialized)
        {
            //FIXME:
            GameManager.Instance.RemoveObserver(this);
        }
    }


    void Update()
    {
        if(characterStats.CurrentHealth == 0)
        {
            isDead = true;
        }
        if (!playerDead)
        {
            SwitchStates();
            SwitchAnimation();
            lastAttackTime -= Time.deltaTime;
        }
    }

    void SwitchAnimation()
    {
        anim.SetBool("Walk", isWalk);
        anim.SetBool("Chase", isChase);
        anim.SetBool("Follow", isFollow);
        anim.SetBool("Critical", characterStats.isCritical);
        anim.SetBool("Death", isDead);
    }

    void SwitchStates()
    {
        if(isDead)
        {
            enemy_states = EnemyStates.DEAD;
        }
        else if(FoundPlayer())
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
                    print("guardPos"+guardPos);
                    print("position" + transform.position);
                    print(transform.position - guardPos);
                    isWalk = true;
                    agent.isStopped = false;
                    agent.destination = guardPos;
                    
                    if(Vector3.SqrMagnitude(guardPos-transform.position) <= agent.stoppingDistance
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
                else{
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
                    agent.isStopped = false;
                    agent.destination = attackTarget.transform.position;
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
                //进入攻击范围切换状态
                if(TargetInAttackRange() || TargetInSkillRange())
                {
                    isFollow = false;
                    agent.isStopped = true;
                    if(lastAttackTime < 0)
                    {
                        lastAttackTime = characterStats.attackData.coolDown;
                        
                        //是否暴击
                        characterStats.isCritical = Random.value < characterStats.attackData.criticalChance;
                        //if(characterStats.isCritical) Debug.Log("暴击");
                        //else Debug.Log("普通攻击");
                        //攻击
                        Attack();
                    }
                }
                break;
            case EnemyStates.DEAD:
                
                coll.enabled = false;
                agent.enabled = false;
                Destroy(gameObject, 2f);
                break;
            default:
                Debug.Log("error in function SwitchStates of class EnemyController!\n");
                enemy_states = EnemyStates.DEAD;
                break;
        }
    }

    bool TargetInAttackRange()
    {
        if(attackTarget != null)
        {
            return Vector3.Distance(attackTarget.transform.position,transform.position) <= characterStats.attackData.attackRange;
        }
        else return false;
    }

    bool TargetInSkillRange()
    {
        if(attackTarget != null)
        {
            return Vector3.Distance(attackTarget.transform.position,transform.position) <= characterStats.attackData.skillRange;
        }
        else return false;
    }

    void Attack()
    {
        transform.LookAt(attackTarget.transform);
        if(TargetInAttackRange())//攻击动画
        {
            anim.SetTrigger("Attack");
        }
        if(TargetInSkillRange())//技能动画
        {
            print("set the trigger Skill!");
            anim.SetTrigger("Skill");
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

    //Animation Event

    void Hit()
    {
        if(attackTarget != null)
        {
            var targetStats = attackTarget.GetComponent<CharacterStats>();
            targetStats.TakeDamage(characterStats, targetStats);
        }
    }

    public void EndNotify()
    {
        //游戏停止
        playerDead = true;
        isChase = false;
        isWalk = false;
        attackTarget = null;
        anim.SetBool("Win", true);
    }
}

