using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 장착아이템 슬롯
/// </summary>
public class EquipSlot : BaseSlot
{
    [SerializeField] private eITEMEQUIP_TYPE equipSlotType;
    [SerializeField] private eARMORTYPE armorSlotType;



    /// <summary>
    /// 좌클릭 => 툴팁, 우클릭 => 해제
    /// </summary>

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (slotData == null || slotData.IsEmpty) return;

        if (eventData.button == PointerEventData.InputButton.Left)
        {
            ShowTooltip();
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            bool success = InventorySystem.Instance.UnequipItem(slotData);
            if (success) ClearSlot();
        }
    }


    /// <summary>
    /// 장착 가능한지 확인
    /// </summary>

    public bool CanEquip(ItemBase item)
    {
        if (item == null || !(item is EquipBase equip)) return false;

        if (equip.equipType != equipSlotType) return false;

        switch (equip.equipType)
        {
            case eITEMEQUIP_TYPE.WEAPON:
                return true;
            case eITEMEQUIP_TYPE.EARING:
                return true;
            case eITEMEQUIP_TYPE.ARMOR:
                if (equip is EquipArmor armor)
                    return armor.armorType == armorSlotType;
                return false;

            default:
                return false;
        }
    }


}
