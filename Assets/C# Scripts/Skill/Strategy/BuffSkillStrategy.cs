using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffSkillStrategy : ISkillStrategy
{
    public void Execute(SkillBase skill, ISkillSystem user, string target)
    {
        //
    }

    public void PreExecute(SkillBase skill, ISkillSystem user)
    {
        Vector3 pos = user.SkillOrigin.position;
        Quaternion rot = user.SkillOrigin.rotation;

        if (skill.IndicatorPrefab != null)
        {
            GameObject fx = GameObject.Instantiate(skill.IndicatorPrefab, pos, rot);
        }
    }
}
