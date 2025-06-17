using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ============================
// 3. �Һ� ������ Ŭ���� (ȸ��/����)
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
                player.stat.Heal(50f); // �ӽ� ��ġ
                Debug.Log("HP ȸ��!");
                break;

            case eITEMCONSUM_TYPE.SCROLL:
                Debug.Log("MP ȸ��!");
                break;

            case eITEMCONSUM_TYPE.BOX:
                Debug.Log("���� ��� (�̱���)");
                break;

            default:
                Debug.LogWarning("�� �� ���� �Һ� ������ Ÿ��");
                break;
        }
    }

    public virtual float GetItemSortData()
    {
        return 0f;
    }
}