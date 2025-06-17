using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword_Weapon : WeaponBase
{
    
    private void Awake()
    {
        skills = new SkillBase[]
        {
            GameManager.tableMgr.Skill.GetSkill(5001),
            GameManager.tableMgr.Skill.GetSkill(5002),

        };

    }       
    public override void Attack(CharacterBase owner)
    {
        //
    }
}
