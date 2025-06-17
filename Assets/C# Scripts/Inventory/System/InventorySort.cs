using System.Collections.Generic;
using UnityEngine;

public partial class InventorySystem
{
    /// <summary>
    /// �̸� ���� �������� ����
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
    /// ����Ʈ�� �ֿ� �ɷ�ġ ���� �������� ���� (��: ����-���ݷ�, ��-���� ��)
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
    /// ���� UI ���� �ݿ� �Լ� (Ÿ�Ժ� ���� ��� ����)
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
    /// ��� ������ ���� �Լ� (����/��/�Ͱ���) - �̸��� or ��ɼ�
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

        if (sortByAvility == 0) // �̸�
            SortByName(typeSlot);

        if (sortByAvility == 1) // ��� ��������
            SortByAvilityDesc(typeSlot);

        else  // ��� ��������
            SortByAvilityAsc(typeSlot);

        RefreshInventoryUI(typeSlot, targetSlots);
    }

    /// <summary>
    /// �Һ� ������ ���� �Լ� (����/��ũ��/��Ÿ) - �̸��� or ��ɼ�
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

        if (sortByAvility == 0) // �̸�
            SortByName(typeSlot);

        if (sortByAvility == 1) // ��� ��������
            SortByAvilityDesc(typeSlot);

        else  // ��� ��������
            SortByAvilityAsc(typeSlot);

        RefreshInventoryUI(typeSlot, targetSlots);
    }
}