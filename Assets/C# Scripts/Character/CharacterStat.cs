using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStat
{
    [Header("Config")]
    protected int level;
    protected float maxHP;
    protected float maxMP;
    protected float currentHP;
    protected float currentMP;

    protected float moveSpeed;
    protected float attackSpeed;

    public float defaultDamage;
}
