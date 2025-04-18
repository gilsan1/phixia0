using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : CharacterBase
{
    [Header("Weapon")]
    [SerializeField] public WeaponHitbox hitBox;

    private void Awake()
    {
        stat = new CharacterStat();
        stat.Init();

        combatSystem = new CombatSystem(stat);

        hitBox = GetComponentInChildren<WeaponHitbox>();
        if (hitBox != null)
            hitBox.owner = this;
    }

    // 애니메이션 이벤트에서 호출
    public void EnableWeaponHitbox()
    {
        hitBox?.Activate();
    }

    // 애니메이션 이벤트에서 호출
    public void DisableWeaponHitbox()
    {
        hitBox?.DeActivate();
    }

    public void R_AttackEnemy()
    {
        Monster target = GameObject.FindAnyObjectByType<Monster>();
        if (target != null)
        {
            combatSystem.RangedAttack(this, target);
        }
    }

    public void S_AttackEnemy()
    {
        Monster target = GameObject.FindAnyObjectByType<Monster>();
        if (target != null)
        {
            // 향후 스킬 로직 추가 예정
        }
    }
}












/*public class Player : CharacterBase
{
    private void Awake()
    {
        stat = new CharacterStat();
        stat.Init();
        combatSystem = new CombatSystem(stat);
    }

    public void M_AttackEnemy()
    {
        Monster target = GameObject.FindObjectOfType<Monster>();
        if (target != null)
        {
            combatSystem.MeleeAttack(this, target);
        }
    }

    public void R_AttackEnemy()
    {
        Monster target = GameObject.FindAnyObjectByType<Monster>();
        if (target != null)
        {
            combatSystem.RangedAttack(this, target);
        }
    }
    public void S_AttackEnemy()
    {
        Monster target = GameObject.FindAnyObjectByType<Monster>();
        if (target != null)
        {
            //
        }
    }
}*/
