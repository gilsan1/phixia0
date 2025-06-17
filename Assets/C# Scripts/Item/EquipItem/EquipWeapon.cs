using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipWeapon : EquipBase, ISortData
{
    public EquipWeapon
    (
        int id, // 아이디
        string name,  // 이름
        eITEMTYPE type,  // 아이템 타입
        int max, // 최대수량
        string desc, // 설명
        string iconPath, // 아이콘 경로
        string prefabPath, // 프리팹 경로
        float atk, // 공격력
        float def, // 방어력
        eITEMEQUIP_TYPE equipType // 장비아이템 타입 = 무기
    ) 

        : base(id, name, type, max, desc, iconPath, prefabPath, atk, def, equipType)
    {
    }

    public override float GetItemSortData()
    {
        return Item_Atk;
    }
}
