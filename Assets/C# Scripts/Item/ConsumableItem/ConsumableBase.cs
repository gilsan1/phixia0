using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ============================
// 3. 소비 아이템 클래스 (회복/버프)
// ============================
public class ConsumableBase : ItemBase, ISortData
{
    public eITEMCONSUM_TYPE ConsumableType { get; protected set; }

    public ConsumableBase(int id, string name, eITEMCONSUM_TYPE consumableType, int max, string desc, string iconPath, string prefabPath)
        : base(id, name, eITEMTYPE.CONSUMABLE, max, desc, iconPath, prefabPath)
    {
        ConsumableType = consumableType;
    }

    public override void UseItem(Player player)
    {
        switch (ConsumableType)
        {
            case eITEMCONSUM_TYPE.POTION:
                player.stat.Heal(50f); // 임시 수치
                Debug.Log("HP 회복!");
                break;

            case eITEMCONSUM_TYPE.SCROLL:
                Debug.Log("MP 회복!");
                break;

            case eITEMCONSUM_TYPE.BOX:
                Debug.Log("버프 사용 (미구현)");
                break;

            default:
                Debug.LogWarning("알 수 없는 소비 아이템 타입");
                break;
        }
    }

    public virtual float GetItemSortData()
    {
        return 0f;
    }
}