using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ============================
// 2. ��� ������ Ŭ���� (�ɷ�ġ ����)
// ============================
public abstract class EquipBase : ItemBase, ISortData
{
    public float Item_Atk;
    public float Item_Def;
    public eITEMEQUIP_TYPE equipType; // ���� OR ��



    protected EquipBase(int id, string name, eITEMTYPE type, int max, string desc, string iconPath, string prefabPath, float atk, float def, eITEMEQUIP_TYPE equipType)
    : base(id, name, type, max, desc, iconPath, prefabPath)
    {
        this.Item_Atk = atk;
        this.Item_Def = def;
        this.equipType = equipType;
    }

    public virtual float GetItemSortData()
    {
        return 0f;
    }
}
