using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eHEAL_TARGET { HP, MP, BOTH }
public class ConsumablePotion : ConsumableBase
{
    public float healPercent;
    public eHEAL_TARGET target;

    public ConsumablePotion(
        int id, string name, int max, string desc, string iconPath, string prefabPath, float healPercent, eHEAL_TARGET target)
        : base(id, name, eITEMCONSUM_TYPE.POTION, max, desc, iconPath, prefabPath)
    {
        this.healPercent = healPercent;
        this.target = target;
    }

    public override void UseItem(Player player)
    {
        if (target == eHEAL_TARGET.HP || target == eHEAL_TARGET.BOTH)
        {
            float value = player.stat.Base_maxHP * (healPercent / 100f);
            player.stat.HP_Heal(value);
        }

        if (target == eHEAL_TARGET.MP || target == eHEAL_TARGET.BOTH)
        {
            float value = player.stat.Base_maxMp * (healPercent / 100f);
            player.stat.MP_Heal(value);
        }

        Debug.Log($"{Item_Name} 사용: {target}를 {healPercent}% 회복");
    }

    public override float GetItemSortData() => healPercent;
}

