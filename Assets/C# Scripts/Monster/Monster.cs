using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : CharacterBase
{
    private MonsterSkill mSkill;



    private void Awake()
    {
        characType = eCHARACTER.eCHARACTER_MONSTER;
        stat = new CharacterStat();
        stat.Init();
        combatSystem = new CombatSystem(stat);
    }
    public void AttackPlayer()
    {
        Player target = GameObject.FindObjectOfType<Player>();
        if (target != null)
        {
            combatSystem.MeleeAttack(this, target);
        }
    }
}
