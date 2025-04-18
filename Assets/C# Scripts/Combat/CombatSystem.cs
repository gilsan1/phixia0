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
        Debug.Log($"{attacker.name} ��������! {damage} ������");
    }

    public void RangedAttack(CharacterBase attacker, CharacterBase target)
    {
        int damage = stat.DEX * 2;
        target.stat.TakeDamage(damage);
        Debug.Log($"{attacker.name} ���Ÿ�����! {damage} ������");
    }

    public void UseSkill(CharacterBase attacker, CharacterBase target, int baseDamage, float scalingFactor)
    {
        int damage = baseDamage + Mathf.RoundToInt(stat.INT * scalingFactor);
        target.stat.TakeDamage(damage);
        Debug.Log($"{attacker.name} ��ų���! {damage} ������");
    }
}
