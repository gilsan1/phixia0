using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkillSystem
{
    Transform SkillOrigin { get; }
    CombatSystem CombatSystem { get; }
    CharacterBase Owner { get; }
}
