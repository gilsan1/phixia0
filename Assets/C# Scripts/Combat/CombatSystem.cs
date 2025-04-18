using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatSystem
{
    private CharacterStat stat;

    public CombatSystem(CharacterStat stat)
    {
        this.stat = stat;
    }

    public void MeleeAttack(CharacterBase attacker, CharacterBase target)
    {
        float damage = stat.physicalAttack;
        target.stat.TakeDamage(damage);
        Debug.Log($"{attacker.name} 근접공격! {damage} 데미지");
    }

    public void RangedAttack(CharacterBase attacker, CharacterBase target)
    {
        int damage = stat.DEX * 2;
        target.stat.TakeDamage(damage);
        Debug.Log($"{attacker.name} 원거리공격! {damage} 데미지");
    }

    public void UseSkill(CharacterBase attacker, CharacterBase target, int baseDamage, float scalingFactor)
    {
        int damage = baseDamage + Mathf.RoundToInt(stat.INT * scalingFactor);
        target.stat.TakeDamage(damage);
        Debug.Log($"{attacker.name} 스킬사용! {damage} 데미지");
    }
}
