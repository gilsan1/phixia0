using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 몬스터 베이스 클래스 - 스킬 및 전투 시스템 인터페이스 구현
/// </summary>
public abstract class MonsterBase : CharacterBase, ISkillSystem
{
    public string monsterID;

    protected SkillBase[] skills;
    public SkillBase[] Skills => skills;


    [Header("HP UI")]
    public GameObject hpBarPrefab;
    public Transform hpBarParent;
    private GameObject hpBarInstance;

    public MonsterFSM monsterFSM;
    private bool isDead = false;


    [Header("Drop Item")]
    [SerializeField] private List<MonsterDrop> dropList = new List<MonsterDrop>();


    public Action<float> onDamaged;
    public Action onDeathAction;


    public Transform SkillOrigin => transform;
    public CombatSystem CombatSystem => combatSystem;
    public CharacterBase Owner => this;

    CombatSystem combatSystem;

    protected override void Awake()
    {
        base.Awake();

        stat = new CharacterStat();
        stat.Init();

        characType = eCHARACTER.MONSTER;
        monsterFSM = GetComponent<MonsterFSM>();
        combatSystem = new CombatSystem(stat);
        characType = eCHARACTER.MONSTER;

        InitSkills();
    }

    protected virtual void Start()
    {
        if (!Shared.enemyList.Contains(this))
        {
            Shared.enemyList.Add(this);
            Debug.Log($"{name}이(가) enemyList에 추가됨");
        }

        StartCoroutine(CheckDeathRoutine());
    }

    protected virtual void OnDisable()
    {
        Shared.enemyList.Remove(this);
    }

    protected abstract void InitSkills();

    /// <summary>
    /// 스킬 사전 실행 (장판 생성 등)
    /// </summary>
    public void UseSkill(int index)
    {
        if (!IsValidSkill(index)) return;

        SkillBase skill = skills[index];

        if (!skill.CanExecute()) return;

        skill.MarkExecute();
        GameManager.Instance.skillExecuter.PreExecute(skill, this); //Shared → GameManager
    }

    /// <summary>
    /// 스킬 실제 효과 적용 및 이펙트 처리
    /// </summary>
    public void TriggerSkillEffect(int index)
    {
        if (!IsValidSkill(index))
        {
            Debug.LogWarning($"Invalid skill index {index} on Monster {name}");
            return;
        }

        SkillBase skill = skills[index];

        if (skill.EffectPrefab != null)
        {
            GameManager.Instance.effectManager.SpawnEffect(skill.EffectPrefab, SkillOrigin.position + SkillOrigin.forward * 2f);
        }
        else
        {
            Debug.LogWarning($"Monster {name}의 스킬 {skill.SkillName}에 EffectPrefab이 없습니다.");
        }

        GameManager.Instance.skillExecuter.Execute(skill, this, "Player");
    }

    /// <summary>
    /// 플레이어를 기본 근접공격
    /// </summary>
    public void AttackPlayer()
    {
        Player target = GameObject.FindObjectOfType<Player>();
        if (target != null)
        {
            combatSystem.MeleeAttack(this, target);
        }
    }

    /// <summary>
    /// HP 바 생성
    /// </summary>
    public void CreateHpBar()
    {
        if (hpBarPrefab == null || hpBarParent == null) return;

        hpBarInstance = Instantiate(hpBarPrefab, hpBarParent);
        hpBarInstance.transform.localScale = Vector3.one * 0.1f;

        var bar = hpBarInstance.GetComponent<UI_HealthBar>();
        bar.target = this;
    }

    /// <summary>
    /// 인덱스 유효성 체크
    /// </summary>
    private bool IsValidSkill(int index)
    {
        return skills != null && index >= 0 && index < skills.Length && skills[index] != null;
    }

    /// <summary>
    /// 몬스터가 죽었나 안 죽었나~
    /// </summary>
    /// <returns></returns>
    private IEnumerator CheckDeathRoutine()
    {
        while (!isDead)
        {
            if (stat.currentHP <= 0)
            {
                isDead = true;
                onDeathAction?.Invoke();
                OnDeath();
                yield break;
            }

            yield return new WaitForSeconds(0.2f);
        }
    }
    protected virtual void OnDeath()
    {
        /// 드롭 아이템 생성하기
        for (int i = 0; i < dropList.Count; i++)
        {
            MonsterDrop drop = dropList[i];

            if (UnityEngine.Random.value > drop.dropProbability) continue; // 확률을 계산


            // 아이템 데이터 로드
            ItemBase itemData = GameManager.tableMgr.armorItem.GetItem(drop.itemId); // ID값을 이용해 Item테이블에서 아이템 정보를 가져옴
            if (itemData == null)
            {
                Debug.LogWarning($"[Drop] 아이템 ID {drop.itemId}에 대한 데이터가 존재하지 않습니다.");
                continue;
            }

            // 프리팹 로드
            GameObject prefab = Resources.Load<GameObject>(itemData.ItemPrefabPath);
            if (prefab == null)
            {
                Debug.LogWarning($"[Drop] 프리팹 경로 {itemData.ItemPrefabPath} 가 유효하지 않습니다.");
                continue;
            }

            int count = UnityEngine.Random.Range(drop.minCount, drop.maxCount + 1); // 지정된 범위 내에서 생성 개수 랜덤으로 지정

            for (int j = 0; j < count; j++)
            {
                Vector3 dropPos = transform.position + UnityEngine.Random.insideUnitSphere * 1f;
                dropPos.y = transform.position.y;

                GameObject go = UnityEngine.Object.Instantiate(prefab, dropPos, Quaternion.identity);
                go.layer = LayerMask.NameToLayer("Item");

                // 드롭 아이템에 데이터 설정
                ItemDrop iw = go.GetComponent<ItemDrop>();
                if (iw == null)
                    iw = go.AddComponent<ItemDrop>();

                iw.SetItem(itemData);
            }

            //
            GameManager.Instance.questSystem.OnKillEvent(monsterID, 1);
        }
    }
}
