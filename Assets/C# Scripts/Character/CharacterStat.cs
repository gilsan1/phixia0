using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterStat
{
    /// <summary>
    /// AttributeStats
    /// </summary>
    public int STR { get; private set; }
    public int DEX { get; private set; }
    public int INT { get; private set; }
    public int LUK { get; private set; }


    /// <summary>
    /// ParameterStats
    /// </summary>
    public float Base_maxHP; // 최대 HP
    public float Base_maxMp; // 최대 MP
    public float Base_Atk; // 공격력
    public float Base_Def; // 방어력

    public float BonusAtk; // 아이템
    public float BonusDef;

    public float Total_Atk => Base_Atk + BonusAtk;
    public float Total_Def => Base_Def + BonusDef;

    public float currentHP { get; private set; }
    public float currentMP { get; private set; }


    public void Init()
    {
        STR = 10;
        DEX = 10;
        INT = 10;
        LUK = 10;

        Base_maxHP = 30f;
        Base_maxMp = 100f;
        currentHP = Base_maxHP;
        currentMP = Base_maxMp;
        Base_Atk = 10f;
        Base_Def = 10f;
    }

    public void TakeDamage(float amount)
    {
        currentHP = Mathf.Max(currentHP - amount, 0f);
        Debug.Log($"HP : {currentHP}");
    }

    public void Heal(float amount)
    {
        currentHP = Mathf.Min(currentHP + amount, Base_maxHP);
    }

    public void ApplyEquipStats(EquipBase item)
    {
        BonusAtk += item.Item_Atk;
        BonusDef += item.Item_Def;
        Debug.Log($"[After Apply] TotalAtk{this.Total_Atk}, TotalDef{this.Total_Def}");
    }
    public void RemoveEquipStats(EquipBase item)
    {
        BonusAtk -= item.Item_Atk;
        BonusDef -= item.Item_Def;
        Debug.Log($"[Remove] item={item.Item_Name}, -Atk:{item.Item_Atk}, -Def:{item.Item_Def},TotalAtk{this.Total_Atk}, TotalDef{this.Total_Def}");
    }

    public void HP_Heal(float amount)
    {
        currentHP = Mathf.Min(currentHP + amount, Base_maxHP);
    }

    public void MP_Heal(float amount)
    {
        currentMP = Mathf.Min(currentMP + amount, Base_maxMp);
    }
}
