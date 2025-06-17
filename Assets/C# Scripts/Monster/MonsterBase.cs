using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� ���̽� Ŭ���� - ��ų �� ���� �ý��� �������̽� ����
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
            Debug.Log($"{name}��(��) enemyList�� �߰���");
        }

        StartCoroutine(CheckDeathRoutine());
    }

    protected virtual void OnDisable()
    {
        Shared.enemyList.Remove(this);
    }

    protected abstract void InitSkills();

    /// <summary>
    /// ��ų ���� ���� (���� ���� ��)
    /// </summary>
    public void UseSkill(int index)
    {
        if (!IsValidSkill(index)) return;

        SkillBase skill = skills[index];

        if (!skill.CanExecute()) return;

        skill.MarkExecute();
        GameManager.Instance.skillExecuter.PreExecute(skill, this); //Shared �� GameManager
    }

    /// <summary>
    /// ��ų ���� ȿ�� ���� �� ����Ʈ ó��
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
            Debug.LogWarning($"Monster {name}�� ��ų {skill.SkillName}�� EffectPrefab�� �����ϴ�.");
        }

        GameManager.Instance.skillExecuter.Execute(skill, this, "Player");
    }

    /// <summary>
    /// �÷��̾ �⺻ ��������
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
    /// HP �� ����
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
    /// �ε��� ��ȿ�� üũ
    /// </summary>
    private bool IsValidSkill(int index)
    {
        return skills != null && index >= 0 && index < skills.Length && skills[index] != null;
    }

    /// <summary>
    /// ���Ͱ� �׾��� �� �׾���~
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
        /// ��� ������ �����ϱ�
        for (int i = 0; i < dropList.Count; i++)
        {
            MonsterDrop drop = dropList[i];

            if (UnityEngine.Random.value > drop.dropProbability) continue; // Ȯ���� ���


            // ������ ������ �ε�
            ItemBase itemData = GameManager.tableMgr.armorItem.GetItem(drop.itemId); // ID���� �̿��� Item���̺��� ������ ������ ������
            if (itemData == null)
            {
                Debug.LogWarning($"[Drop] ������ ID {drop.itemId}�� ���� �����Ͱ� �������� �ʽ��ϴ�.");
                continue;
            }

            // ������ �ε�
            GameObject prefab = Resources.Load<GameObject>(itemData.ItemPrefabPath);
            if (prefab == null)
            {
                Debug.LogWarning($"[Drop] ������ ��� {itemData.ItemPrefabPath} �� ��ȿ���� �ʽ��ϴ�.");
                continue;
            }

            int count = UnityEngine.Random.Range(drop.minCount, drop.maxCount + 1); // ������ ���� ������ ���� ���� �������� ����

            for (int j = 0; j < count; j++)
            {
                Vector3 dropPos = transform.position + UnityEngine.Random.insideUnitSphere * 1f;
                dropPos.y = transform.position.y;

                GameObject go = UnityEngine.Object.Instantiate(prefab, dropPos, Quaternion.identity);
                go.layer = LayerMask.NameToLayer("Item");

                // ��� �����ۿ� ������ ����
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
