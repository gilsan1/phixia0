using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipArmor : EquipBase, ISortData
{
    public eARMORTYPE armorType;

    public EquipArmor
    (
        int id, // ���̵�
        string name, // �̸�
        eITEMTYPE type, // ���Ÿ��
        int max, // ����
        string desc, // ����
        string iconPath, // ������ ���
        string prefabPath, // ������ ���
        float atk, // ���ݷ�
        float def, // ����
        eITEMEQUIP_TYPE equipType, // �������� Ÿ�� = �� 
        eARMORTYPE armorType // �� Ÿ��
    )

    : base(id, name, type, max, desc, iconPath, prefabPath, atk, def, equipType)
    {
        this.armorType = armorType;
    }

    public override float GetItemSortData() => Item_Def;

}
