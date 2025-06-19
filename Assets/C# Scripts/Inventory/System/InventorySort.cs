using System.Collections.Generic;
using UnityEngine;

public partial class InventorySystem
{
    /// <summary>
    /// 이름 기준 오름차순 정렬
    /// </summary>
    private void SortByName(List<ItemSlot> list)
    {
        for (int i = 0; i < list.Count - 1; i++)
        {
            for (int j = 0; j < list.Count - i - 1; j++)
            {
                string a = list[j].Item.Item_Name;
                string b = list[j + 1].Item.Item_Name;
                if (string.Compare(a, b) > 0)
                {
                    ItemSlot temp = list[j];
                    list[j] = list[j + 1];
                    list[j + 1] = temp;
                }
            }
        }
    }

    /// <summary>
    /// 리스트를 주요 능력치 기준 내림차순 정렬 (예: 무기-공격력, 방어구-방어력 등)
    /// </summary>
    private void SortByAvilityDesc(List<ItemSlot> list)
    {
        for (int i = 0; i < list.Count - 1; i++)
        {
            for (int j = 0; j < list.Count - i - 1; j++)
            {
                float a = (list[j].Item as ISortData)?.GetItemSortData() ?? 0f;
                float b = (list[j + 1].Item as ISortData)?.GetItemSortData() ?? 0f;
                if (a < b)
                {
                    var temp = list[j];
                    list[j] = list[j + 1];
                    list[j + 1] = temp;
                }
            }
        }
    }

    private void SortByAvilityAsc(List<ItemSlot > list)
    {
        for (int i = 0; i < list.Count - 1; i++)
        {
            for (int j = 0;j < list.Count - i - 1; j++)
            {
                float a = (list[j].Item as ISortData)?.GetItemSortData() ?? 0f;
                float b = (list[j + 1].Item as ISortData)?.GetItemSortData() ?? 0f;

                if (a > b)
                {
                    var temp = list[j];
                    list[j] = list[j + 1];
                    list[j + 1] = temp;
                }
            }
        }
    }

    /// <summary>
    /// 공통 UI 정렬 반영 함수 (타입별 슬롯 대상만 갱신)
    /// </summary>
    private void RefreshInventoryUI(List<ItemSlot> sorted, List<InventorySlot> targetSlots)
    {
        if (targetSlots == null) return;

        int i = 0;
        for (; i < sorted.Count && i < targetSlots.Count; i++)
        {
            targetSlots[i].SetSlot(sorted[i]);
        }

        for (; i < targetSlots.Count; i++)
        {
            targetSlots[i].ClearSlot();
        }
    }

    /// <summary>
    /// 장비 아이템 정렬 함수 (무기/방어구/귀걸이) - 이름순 or 기능순
    /// </summary>
    public void SortEquipItem(eITEMEQUIP_TYPE typeFilter, int sortByAvility)
    {
        List<ItemSlot> typeSlot = new List<ItemSlot>();
        List<InventorySlot> targetSlots = null;

        for (int i = 0; i < inventoryData.Count; i++)
        {
            if (inventoryData[i].Item is EquipBase equip && equip.equipType == typeFilter)
                typeSlot.Add(inventoryData[i]);
        }

        switch (typeFilter)
        {
            case eITEMEQUIP_TYPE.WEAPON: targetSlots = weaponSlots; break;
            case eITEMEQUIP_TYPE.ARMOR: targetSlots = armorSlots; break;
            case eITEMEQUIP_TYPE.EARING: targetSlots = earingSlots; break;
        }

        if (sortByAvility == 0) // 이름
            SortByName(typeSlot);

        if (sortByAvility == 1) // 기능 내림차순
            SortByAvilityDesc(typeSlot);

        else  // 기능 오름차순
            SortByAvilityAsc(typeSlot);

        RefreshInventoryUI(typeSlot, targetSlots);
    }

    /// <summary>
    /// 소비 아이템 정렬 함수 (포션/스크롤/기타) - 이름순 or 기능순
    /// </summary>
    public void SortConsumableItem(eITEMCONSUM_TYPE typeFilter, int sortByAvility)
    {
        List<ItemSlot> typeSlot = new List<ItemSlot>();
        List<InventorySlot> targetSlots = null;

        for (int i = 0; i < inventoryData.Count; i++)
        {
            if (inventoryData[i].Item is ConsumableBase consum && consum.ConsumableType == typeFilter)
                typeSlot.Add(inventoryData[i]);
        }

        switch (typeFilter)
        {
            case eITEMCONSUM_TYPE.POTION: targetSlots = potionSlots; break;
            case eITEMCONSUM_TYPE.SCROLL: targetSlots = scrollSlots; break;
            case eITEMCONSUM_TYPE.BOX: targetSlots = boxSlots; break;
        }

        if (sortByAvility == 0) // 이름
            SortByName(typeSlot);

        if (sortByAvility == 1) // 기능 내림차순
            SortByAvilityDesc(typeSlot);

        else  // 기능 오름차순
            SortByAvilityAsc(typeSlot);

        RefreshInventoryUI(typeSlot, targetSlots);
    }
}