using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCombat : ICharacterCombat
{
    public CharacterStat stat;

    public void Attack(CharacterBase attacker, CharacterBase target)
    {
        target.stat.TakeDamage(attacker.stat.damage);
    }
}
