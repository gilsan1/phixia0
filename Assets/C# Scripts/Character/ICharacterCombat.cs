using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacterCombat
{
    void Attack(CharacterBase attacker, CharacterBase target);
}
