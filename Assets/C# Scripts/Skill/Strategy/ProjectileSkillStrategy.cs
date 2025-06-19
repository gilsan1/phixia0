using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ProjectileSkillStrategy : ISkillStrategy
{
    public void PreExecute(SkillBase skill, ISkillSystem user)
    {
        Vector3 pos = user.SkillOrigin.position;
        Quaternion rot = user.SkillOrigin.rotation;

        if (skill.IndicatorPrefab != null)
        {
            GameObject castFx = GameObject.Instantiate(skill.IndicatorPrefab, pos, rot);
        }
    }

    public void Execute(SkillBase skill, ISkillSystem user, string target)
    {
        Vector3 pos = user.SkillOrigin.position;
        Quaternion rot = user.SkillOrigin.rotation;

        GameObject projectile = GameObject.Instantiate(skill.EffectPrefab, pos, rot);

        if (projectile.TryGetComponent(out ProjectileController contoller))
        {
            contoller.Launch(user, skill, target);
        }
        else
        {
            Debug.LogWarning("[ProjectileSkillStrategy] EffectPrefab에 ProjectileController가 없습니다.");
        }
    }
}
