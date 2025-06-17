using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkillStrategy
{
    void PreExecute(SkillBase skill, ISkillSystem user);

    void Execute(SkillBase skill, ISkillSystem user, string target);
}
