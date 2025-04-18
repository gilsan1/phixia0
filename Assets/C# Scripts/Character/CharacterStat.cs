using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterStat
{
    public int STR { get; private set; }
    public int DEX { get; private set; }
    public int INT { get; private set; }
    public int VIT { get; private set; }

    public float maxHP;
    public float maxMP;
    public float physicalAttack;
    public float magicAttack;
    public float critChance;

    public float currentHP { get; private set; }
    public float currentMP { get; private set; }

    public void Init()
    {
        maxHP = 100f;
        maxMP = 100f;
        currentHP = maxHP;
        currentMP = maxMP;
        physicalAttack = 10f;
    }

    public void TakeDamage(float amount)
    {
        currentHP = Mathf.Max(currentHP - amount, 0f);
    }

    public void Heal(float amount)
    {
        currentHP = Mathf.Min(currentHP + amount, maxHP);
    }
}
