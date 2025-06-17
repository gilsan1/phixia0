using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

/// <summary>
/// 몬스터 마법사 클래스 - 고유 스킬 설정
/// </summary>
public class Monster_Magician : MonsterBase
{
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void InitSkills()
    {
        skills = new SkillBase[]
        {
            new MeleeSkill(
                "Explosion",
                damage: 20f,
                cooldown: 10f,
                range: 10f,
                angle: 60f,
                indicator: eINDICATOR.FAN,
                effect: Resources.Load<GameObject>("Effect/VFX_Enemy1/Explosion"),
                iconPath: "Icons/Skill/Skill_Sword",
                IndicatorFactory.LoadIndicator(eINDICATOR.FAN)
            ),

            new MagicSkill(
                "MagicField",
                damage: 10f,
                cooldown: 10f,
                range: 10f,
                angle: 10f,
                indicator: eINDICATOR.CIRCLE,
                effect: Resources.Load<GameObject>("Effect/VFX_Enemy1/MagicField"),
                iconPath: "Icons/Skill/Skill_Sword",
                IndicatorFactory.LoadIndicator(eINDICATOR.CIRCLE)
            )
        };
    }

    private void Start()
    {
        base.Start();
        CreateHpBar();
    }
}
