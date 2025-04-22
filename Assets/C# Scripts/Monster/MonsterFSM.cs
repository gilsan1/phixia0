using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFSM : MonoBehaviour
{
    public eMonster_STATE currentState;

    [SerializeField] private float chaseRange = 10f;
    [SerializeField] private float attackRange = 5f;
    [SerializeField] Transform target;

    private Animator animator;
    private Monster monster;

    [SerializeField] private Transform detectPoint;

    float detectRange = 10f;       // 거리 조건
    float viewAngle = 60f;

    private Coroutine stateRoutine;

    private void Awake()
    {
        monster = GetComponent<Monster>();
        animator = GetComponent<Animator>();
        currentState = eMonster_STATE.IDLE;
        target = FindObjectOfType<Player>().transform;
    }

    private void Start()
    {
        ChangeState(eMonster_STATE.IDLE);
    }

    public void ChangeState(eMonster_STATE state)
    {
        if (stateRoutine != null)
            StopCoroutine(stateRoutine);

        currentState = state;

        switch (currentState)
        {
            case eMonster_STATE.IDLE:
                stateRoutine = StartCoroutine(IdleState());
                break;
            case eMonster_STATE.CHASE:
                stateRoutine = StartCoroutine(ChaseState());
                break;
            case eMonster_STATE.ATTACK:
                stateRoutine = StartCoroutine(AttackState());
                break;
        }
    }

    IEnumerator IdleState()
    {

        while (true)
        {
            animator.SetInteger("STATE", (int)eMonster_STATE.IDLE);

            Vector3 dir = (target.position - detectPoint.position).normalized;
            float dist = Vector3.Distance(detectPoint.position, target.position);
            float angle = Vector3.Angle(detectPoint.forward, dir);

            if (dist < chaseRange && angle < 60f / 2f)
            {
                ChangeState(eMonster_STATE.CHASE);
                yield break;
            }

            yield return null;
        }
        /* while (true)
         {
             animator.SetInteger("STATE", (int)eMonster_STATE.IDLE);

             if (Vector3.Distance(transform.position, target.position) < chaseRange)
             {
                 ChangeState(eMonster_STATE.CHASE);
                 yield break;
             }

             yield return null;
         }*/
    }


    // 추격 상태
    IEnumerator ChaseState()
    {
        while (true)
        {
            animator.SetInteger("STATE", (int)eMonster_STATE.CHASE);

            Vector3 direction = (target.position - detectPoint.position).normalized;
            //float distance = Vector3.Distance(detectPoint.position, target.position);

            Vector3 dir = (target.position - detectPoint.position).normalized;
            float dist = Vector3.Distance(detectPoint.position, target.position);
            float angle = Vector3.Angle(detectPoint.forward, dir);


            if (dist > chaseRange && angle < 60f / 2f)
            {
                ChangeState(eMonster_STATE.IDLE);
                yield break;
            }

            if (dist <= attackRange && angle < 60f / 2f)
            {
                ChangeState(eMonster_STATE.ATTACK);
                yield break;
            }

            float stopDistance = 1f;

            if(dist > stopDistance)
            {
                Vector3 targetPosition = target.position - direction * stopDistance;
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * 2f);
            }

            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);

            yield return null;
        }
    }


    // 공격 상태
    IEnumerator AttackState()
    {
        while (true)
        {
            float distance = Vector3.Distance(detectPoint.position, target.position);

            if (distance > attackRange)
            {
                ChangeState(eMonster_STATE.CHASE);
                yield break;
            }

            animator.SetInteger("STATE", (int)eMonster_STATE.ATTACK);
            Debug.Log("플레이어 공격");
            monster.combatSystem.MeleeAttack(monster, target.GetComponent<CharacterBase>());

            yield return new WaitForSeconds(1f);
        }
    }


    private void OnDrawGizmosSelected()
    {
        Vector3 left = Quaternion.Euler(0, -viewAngle / 2f, 0) * transform.forward;
        Vector3 right = Quaternion.Euler(0, viewAngle / 2f, 0) * transform.forward;

        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(detectPoint.position, left * attackRange);
        Gizmos.DrawRay(detectPoint.position, right * attackRange);


        Gizmos.color = Color.red;
        Gizmos.DrawRay(detectPoint.position, left * chaseRange);
        Gizmos.DrawRay(detectPoint.position, right * chaseRange);
    }
}

