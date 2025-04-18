using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICombat
{
    public interface ICombat
    {
        void MeleeAttack(CharacterBase attacker, CharacterBase target);
        void RangedAttack(CharacterBase attacker, CharacterBase target);
        void UseSkill(CharacterBase attacker, CharacterBase target, int baseDamage, float scalingFactor);
    }
}
