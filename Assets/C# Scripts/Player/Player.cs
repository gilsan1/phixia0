using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;
using System;

public partial class Player : CharacterBase, ISkillSystem
{
    [Header("Weapon")]
    [SerializeField] public WeaponHitbox hitBox;
    [SerializeField] public WeaponBase currentWeapon;
    [SerializeField] public Transform effectTransform;

    [Header("Inventory")]
    public Inventory<ItemBase> inventory;

    public Transform SkillOrigin => this.transform;
    public CombatSystem CombatSystem => combatSystem;
    public CharacterBase Owner => this;

    private CombatSystem combatSystem;


    //Action 이벤트 선언: 외부에서 스킬 사용에 반응 가능
    public Action<int> onSkillUsed;
    public Action<float> onDamaged;
    public Action onDeath;

    
    public eINTERACTIONSTATE interacState { get; private set; } = eINTERACTIONSTATE.NONE;

    public void SetInteractionState(eINTERACTIONSTATE state)
    {
        interacState = state;
    }

    protected override void Awake()
    {
        base.Awake(); // 공통 초기화

        stat = new CharacterStat();
        stat.Init();

        characType = eCHARACTER.PLAYER;

        combatSystem = new CombatSystem(stat);

        currentWeapon = GetComponentInChildren<WeaponBase>();
        hitBox = GetComponentInChildren<WeaponHitbox>();
        if (hitBox != null) hitBox.owner = this;

        Shared.SetPlayer(this);
        
    }

    private void Start()
    {
        //inventory = GameManager.Instance.uiManager.inventory.GetComponent<Inventory<ItemBase>>();
        StartCoroutine(CheckDeathRoutine());
    }


    /// 무기 히트박스 활성화
    public void EnableWeaponHitbox() => hitBox?.Activate();


    /// 무기 히트박스 비활성화
    public void DisableWeaponHitbox() => hitBox?.DeActivate();


    /// 무기 공격 실행
    public void Attack()
    {
        currentWeapon?.Attack(this);
    }


    /// 스킬 쿨타임 처리 
    public void UseSkill(int index)
    {
        if (currentWeapon == null || currentWeapon.Skills.Length <= index) return;

        SkillBase skill = currentWeapon.Skills[index];

        if (!skill.CanExecute()) return;

        skill.MarkExecute();

        // UI, 스킬은 GameManager가
        GameManager.Instance.uiManager.TriggerSkillCooldown(index); // 스킬 쿨타임
        GameManager.Instance.skillExecuter.PreExecute(skill, this);

        onSkillUsed?.Invoke(index);

    }

    /// <summary>
    /// 스킬 이펙트 생성 및 스킬 실행
    /// </summary>
    public void TriggerSkillEffect(int index)
    {
        if (currentWeapon == null || index >= currentWeapon.Skills.Length) return;

        SkillBase skill = currentWeapon.Skills[index];
        Vector3 fxPos = (effectTransform != null) ? effectTransform.position : transform.position + transform.forward;

        GameManager.Instance.effectManager.SpawnEffect(skill.EffectPrefab, fxPos);

        GameManager.Instance.skillExecuter.Execute(skill, this, "Enemy");
    }

    /// <summary>
    /// 외부에서 호출 시 데미지 처리 후 이벤트 발생
    /// </summary>
    public void TakeDamage(float amount)
    {
        stat.TakeDamage(amount);
        onDamaged?.Invoke(amount);
    }

    /// <summary>
    /// 체력 감소 확인 후 사망 처리
    /// </summary>
    private IEnumerator CheckDeathRoutine()
    {
        while (true)
        {
            if (stat.currentHP <= 0)
            {
                onDeath?.Invoke();
                break;
            }
            yield return new WaitForSeconds(0.2f);
        }
    }

}