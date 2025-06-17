using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableScroll : ConsumableBase, ISortData
{
    public eBUFF_TYPE buffType;
    public float buffAmount;
    public float buffDuration;

    public ConsumableScroll(int id, string name, int max, string desc, string iconPath, string prefabPath, eBUFF_TYPE buffType, float buffAmount, float buffDuration)
     : base(id, name, eITEMCONSUM_TYPE.SCROLL, max, desc, iconPath, prefabPath)
    {
        this.buffType = buffType;
        this.buffAmount = buffAmount;
        this.buffDuration = buffDuration;
    }


    public override void UseItem(Player player)
    {
        if (Shared.buffMgr == null)
        {
            Debug.Log("BuffManager is null");
            return;
        }
        Shared.buffMgr?.ApplyBuff(buffType, buffAmount, buffDuration);
        Debug.Log($"{Item_Name} 사용: {buffType} +{buffAmount} ({buffDuration}초간)");

    }

    public override float GetItemSortData()
    {
        return buffAmount;
    }
}
