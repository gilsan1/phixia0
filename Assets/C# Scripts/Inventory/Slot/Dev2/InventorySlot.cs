using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : BaseSlot
{
    /// <summary>
    /// 타입 구분
    /// </summary>
    [Header("ITEMTYPE")]
    [SerializeField] private eITEMTYPE itemType;
    [SerializeField] private eITEMEQUIP_TYPE equipType;
    [SerializeField] private eITEMCONSUM_TYPE consumableType;

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (slotData == null || slotData.IsEmpty) return;

        if (eventData.button == PointerEventData.InputButton.Left)
        {
            GameManager.Instance.uiManager.ItemTooltip.ShowTooltip(slotData.Item, this.GetComponentInParent<Canvas>().transform);
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (slotData.Item.Item_Type == eITEMTYPE.EQUIP)
            {
                bool success = InventorySystem.Instance.EquipItem(slotData, out BaseSlot equippedSlot);
                if (success) ClearSlot();
            }
            else if (slotData.Item.Item_Type == eITEMTYPE.CONSUMABLE)
            {
                if (slotData.Item is ConsumableBase consumable)
                {
                    slotData.Item.UseItem(Shared.player_);
                    slotData.Remove(1);

                    if (slotData.IsEmpty)
                        ClearSlot();
                    else
                        SetSlot(slotData); // 수량 반영
                }
            }
        }
    }

    public bool Accepts(ItemBase item)
    { 
        if (item == null) return false;

        if (item.Item_Type != itemType) return false;

        switch (itemType)
        {
            case eITEMTYPE.EQUIP:
                return item is EquipBase equip && equip.equipType == equipType;
            case eITEMTYPE.CONSUMABLE:
                return item is ConsumableBase consumable && consumable.ConsumableType == consumableType;
            case eITEMTYPE.ETC:
                return true;

            default:
                return false;
        }
    }

    public eITEMTYPE GetItemType() => itemType;

    public eITEMEQUIP_TYPE GetEquipType() => equipType;

    public eITEMCONSUM_TYPE GetConsumableType() => consumableType;

}
