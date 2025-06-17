using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipArmor : EquipBase, ISortData
{
    public eARMORTYPE armorType;

    public EquipArmor
    (
        int id, // 아이디
        string name, // 이름
        eITEMTYPE type, // 장비타입
        int max, // 수량
        string desc, // 설명
        string iconPath, // 아이콘 경로
        string prefabPath, // 프리팹 경로
        float atk, // 공격력
        float def, // 방어력
        eITEMEQUIP_TYPE equipType, // 장비아이템 타입 = 방어구 
        eARMORTYPE armorType // 방어구 타입
    )

    : base(id, name, type, max, desc, iconPath, prefabPath, atk, def, equipType)
    {
        this.armorType = armorType;
    }

    public override float GetItemSortData() => Item_Def;

}
