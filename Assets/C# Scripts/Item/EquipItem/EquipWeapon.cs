using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipWeapon : EquipBase, ISortData
{
    public EquipWeapon
    (
        int id, // ���̵�
        string name,  // �̸�
        eITEMTYPE type,  // ������ Ÿ��
        int max, // �ִ����
        string desc, // ����
        string iconPath, // ������ ���
        string prefabPath, // ������ ���
        float atk, // ���ݷ�
        float def, // ����
        eITEMEQUIP_TYPE equipType // �������� Ÿ�� = ����
    ) 

        : base(id, name, type, max, desc, iconPath, prefabPath, atk, def, equipType)
    {
    }

    public override float GetItemSortData()
    {
        return Item_Atk;
    }
}
