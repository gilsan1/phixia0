using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterStat : MonoBehaviour
{
    public int STR;
    public int DEX;
    public int INT;
    public int LUK;

    public int level;
    public float maxHp;
    public float maxMp;
    public float currentHp;
    public float currentMp;
    public float damage;

    public void TakeDamage(float damage)
    {
        currentHp -= damage;
    }

    public void SetStat(int level, float hp, float mp, float damage)
    {
        this.level = level;
        this.maxHp = hp;
        this.maxMp = mp;
        this.currentHp = maxHp;
        this.currentMp = maxMp;
        this.damage = damage;
    }
}
