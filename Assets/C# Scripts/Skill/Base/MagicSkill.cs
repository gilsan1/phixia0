using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MagicSkill : SkillBase
{
    public override eSKILL_TYPE skillType => eSKILL_TYPE.MAGIC;

    public MagicSkill(string name, float damage, float cooldown, float range, float angle, eINDICATOR indicator, GameObject effect, string iconPath, GameObject indicatorPrefab = null)
    {
        SkillName = name;

        Damage = damage;

        CoolDown = cooldown;

        Range = range;

        Angle = angle;

        indicatorType = indicator;

        EffectPrefab = effect;
        IndicatorPrefab = indicatorPrefab;

        IconPath = iconPath;
    }
}
