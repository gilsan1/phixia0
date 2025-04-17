using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stat_", menuName = "Character/Stat Template")]
public class StatTemplate : ScriptableObject
{
    public int level;
    public float maxHP;
    public float maxMP;

    public float attackDamage;
    public float attackSpeed;

    public float currentHP;
}
