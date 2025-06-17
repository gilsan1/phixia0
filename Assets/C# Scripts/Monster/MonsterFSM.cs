using UnityEngine;
using System.Collections;

public class MonsterFSM : AIBase<eMONSTER_STATE>
{
    [SerializeField] private float chaseRange = 10f;
    [SerializeField] private float attackRange = 5f;
    [SerializeField] private Transform detectPoint;

    private Transform target;
    private MonsterAnimation monsterAnimator;
    private MonsterBase monster;

    private void Awake()
    {
        monster = GetComponent<MonsterBase>();
        monsterAnimator = GetComponent<MonsterAnimation>();
        target = Shared.player_?.transform;
    }

    private void Start()
    {
        StartCoroutine(InitStart());
    }

    private IEnumerator InitStart()
    {
        yield return new WaitUntil(() => Shared.player_ != null);
        target = Shared.player_.transform;
        ChangeState(eMONSTER_STATE.IDLE);
    }

    protected override IEnumerator RunState(eMONSTER_STATE state)
    {
        switch (state)
        {
            case eMONSTER_STATE.IDLE:
                yield return StartCoroutine(IdleState());
                break;
            case eMONSTER_STATE.CHASE:
                yield return StartCoroutine(ChaseState());
                break;
            case eMONSTER_STATE.ATTACK:
                yield return StartCoroutine(AttackState());
                break;
            case eMONSTER_STATE.SKILL1:
                yield return StartCoroutine(SkillState(0));
                break;
            case eMONSTER_STATE.SKILL2:
                yield return StartCoroutine(SkillState(1));
                break;
            case eMONSTER_STATE.DIE:
                yield return StartCoroutine(DieState());
                break;
            default:
                yield break;
        }
    }

    private IEnumerator IdleState()
    {
        monsterAnimator.SetState(eMONSTER_STATE.IDLE);
        while (true)
        {
            if (target != null && Vector3.Distance(transform.position, target.position) < chaseRange)
            {
                ChangeState(eMONSTER_STATE.CHASE);
                yield break;
            }
            yield return null;
        }
    }

    private IEnumerator ChaseState()
    {
        monsterAnimator.SetState(eMONSTER_STATE.CHASE);
        while (true)
        {
            if (target == null)
            {
                ChangeState(eMONSTER_STATE.IDLE);
                yield break;
            }

            float dist = Vector3.Distance(transform.position, target.position);
            if (dist < attackRange)
            {
                ChangeState(eMONSTER_STATE.ATTACK);
                yield break;
            }

            Vector3 dir = (target.position - transform.position).normalized;
            transform.position += dir * Time.deltaTime * 2f;

            yield return null;
        }
    }

    private IEnumerator AttackState()
    {
        monsterAnimator.SetState(eMONSTER_STATE.ATTACK);

        float delay = GetNextSkillDelay();
        yield return new WaitForSeconds(delay);

        if (CanUseSkill(0))
        {
            ChangeState(eMONSTER_STATE.SKILL1);
            yield break;
        }

        if (CanUseSkill(1))
        {
            ChangeState(eMONSTER_STATE.SKILL2);
            yield break;
        }

        ChangeState(eMONSTER_STATE.ATTACK);
    }

    private IEnumerator SkillState(int index)
    {
        monsterAnimator.SetState(index == 0 ? eMONSTER_STATE.SKILL1 : eMONSTER_STATE.SKILL2);

        Vector3 dir = (target.position - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(dir);

        monster.UseSkill(index);
        monster.Skills[index].lastUsedTime = Time.time;

        float animDuration = GetAnimationDuration(index);
        yield return new WaitForSeconds(animDuration);

        ChangeState(eMONSTER_STATE.ATTACK);
    }

    private IEnumerator DieState()
    {
        monsterAnimator.SetState(eMONSTER_STATE.DIE);
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

    // Skill usable check
    private bool CanUseSkill(int index)
    {
        if (monster.Skills == null || index >= monster.Skills.Length || monster.Skills[index] == null)
            return false;

        return monster.Skills[index].CanExecute();
    }

    // Skill cooldown remaining time
    private float GetCooldownRemaining(int index)
    {
        if (monster.Skills == null || index >= monster.Skills.Length || monster.Skills[index] == null)
            return float.MaxValue;

        float remain = monster.Skills[index].lastUsedTime + monster.Skills[index].CoolDown - Time.time;
        return Mathf.Max(0f, remain);
    }

    // Skill delay logic
    private float GetNextSkillDelay()
    {
        float d1 = GetCooldownRemaining(0);
        float d2 = GetCooldownRemaining(1);

        if (d1 == 0f || d2 == 0f)
            return 3f;
        else
            return Mathf.Min(d1, d2);
    }

    // Animation length lookup
    private float GetAnimationDuration(int skillIndex)
    {
        string clipName = skillIndex == 0 ? "Enemy_Skill1" : "Enemy_Skill2";
        var ac = monsterAnimator.animator.runtimeAnimatorController;
        var clips = ac.animationClips;

        for (int i = 0; i < clips.Length; i++)
        {
            if (clips[i].name == clipName)
                return clips[i].length;
        }
        return 1f;
    }
}
