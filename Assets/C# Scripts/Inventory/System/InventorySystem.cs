using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;


/// <summary>
/// 인벤토리 및 장비 시스템을

/// - 장착 / 해제 / 아이템 추가 로직 전담
/// - 능력치 반영 및 외형 스폰 처리 포함

/// </summary>
public partial class InventorySystem : MonoBehaviour
{
    public static InventorySystem Instance { get; private set; }

    [Header("인벤토리 슬롯")]
    [SerializeField] private List<InventorySlot> invenSlots;

    [Header("장착 슬롯")]
    [SerializeField] private List<EquipSlot> equipSlots;


    [Header("슬롯 세분화")]
    private List<InventorySlot> weaponSlots = new List<InventorySlot>();
    private List<InventorySlot> armorSlots = new List<InventorySlot>();
    private List<InventorySlot> earingSlots = new List<InventorySlot>();
    private List<InventorySlot> potionSlots = new List<InventorySlot>();
    private List<InventorySlot> scrollSlots = new List<InventorySlot>();
    private List<InventorySlot> boxSlots = new List<InventorySlot>();
    private List<InventorySlot> etcSlots = new List<InventorySlot>();


    /// <summary>
    /// 정렬용 리스트?
    /// </summary>
    private List<ItemSlot> inventoryData = new List<ItemSlot>();


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        invenSlots = new List<InventorySlot>(GetComponentsInChildren<InventorySlot>(true));
        equipSlots = new List<EquipSlot>(GetComponentsInChildren<EquipSlot>(true));
        InitSlots();
    }


    private void InitSlots()
    {
        weaponSlots.Clear();
        armorSlots.Clear();
        earingSlots.Clear();
        potionSlots.Clear();
        scrollSlots.Clear();
        boxSlots.Clear();
        etcSlots.Clear();


        for (int i = 0; i < invenSlots.Count; i++)
        {
            InventorySlot slot = invenSlots[i];

            switch (slot.GetItemType())
            {
                case eITEMTYPE.EQUIP:
                    switch (slot.GetEquipType())
                    {
                        case eITEMEQUIP_TYPE.WEAPON: weaponSlots.Add(slot); break;
                        case eITEMEQUIP_TYPE.ARMOR: armorSlots.Add(slot); break;
                        case eITEMEQUIP_TYPE.EARING: earingSlots.Add(slot); break;
                    }
                    break;
                case eITEMTYPE.CONSUMABLE:
                    switch (slot.GetConsumableType())
                    {
                        case eITEMCONSUM_TYPE.POTION: potionSlots.Add(slot); break;
                        case eITEMCONSUM_TYPE.SCROLL: scrollSlots.Add(slot); break;
                        case eITEMCONSUM_TYPE.BOX: boxSlots.Add(slot); break;
                    }
                    break;
                case eITEMTYPE.ETC:
                    etcSlots.Add(slot);
                    break;
            }
        }
    }


    public bool EquipItem(ItemSlot itemslot, out BaseSlot equippedSlot)
    {
        equippedSlot = null;
        if (itemslot == null || itemslot.IsEmpty) return false;
        if (itemslot.Item is not EquipBase equipData) return false;

        for (int i = 0; i < equipSlots.Count; i++)
        {
            EquipSlot slot = equipSlots[i];

            if (!slot.CanEquip(itemslot.Item)) continue;

            // 기존 장비가 있다면 → 가장 앞 빈 슬롯에 추가
            if (slot.slotData != null && !slot.slotData.IsEmpty && slot.slotData.Item is EquipBase prevEquip)
            {
                Shared.player_.stat.RemoveEquipStats(prevEquip);

                if (prevEquip.equipType == eITEMEQUIP_TYPE.WEAPON)
                    PlayerEquip.Instance.ClearWeapon();
                else if (prevEquip.equipType == eITEMEQUIP_TYPE.ARMOR)
                    PlayerEquip.Instance.ClearHelmet();

                // 가장 앞 빈 슬롯에 무조건 추가
                TryAddItem(prevEquip);
            }

            // 새 장비 반영
            Shared.player_.stat.ApplyEquipStats(equipData);
            Shared.player_.StatChanged();

            if (equipData.equipType == eITEMEQUIP_TYPE.WEAPON)
                PlayerEquip.Instance.SpawnWeapon(equipData.LoadPrefab());
            else if (equipData.equipType == eITEMEQUIP_TYPE.ARMOR)
                PlayerEquip.Instance.SpawnHelmet(equipData.LoadPrefab());

            slot.SetSlot(itemslot);
            equippedSlot = slot;

            return true;
        }

        return false;
    }

    /// <summary>
    /// 아이템 해제 처리 (능력치 제거 + 외형 제거)
    /// </summary>
    public bool UnequipItem(ItemSlot itemSlot)
    {
        if (itemSlot == null || itemSlot.IsEmpty) return false;
        if (itemSlot.Item is not EquipBase equipItem) return false;

        Shared.player_.stat.RemoveEquipStats(equipItem);
        Shared.player_.StatChanged();

        // 인벤토리 슬롯 중 장비 아이템을 받을 수 있는 첫 빈 슬롯 찾기

        if (equipItem.equipType == eITEMEQUIP_TYPE.WEAPON)
            PlayerEquip.Instance.ClearWeapon();
        else if (equipItem.equipType == eITEMEQUIP_TYPE.ARMOR)
            PlayerEquip.Instance.ClearHelmet();

        var targetSlot = GetTargetSlot(equipItem);

        for (int i = 0; i < targetSlot.Count; i++)
        {
            var slot = targetSlot[i];
            if ((slot.slotData == null || slot.slotData.IsEmpty) && slot.Accepts(equipItem))
            {
                slot.SetSlot(itemSlot);
                return true;
            }
        }

        Debug.Log("빈 슬롯이 없습니다. 해제 실패");
        return false;
    }

    

    /// <summary>
    /// 인벤토리 빈 슬롯에 아이템 추가 시도
    /// </summary>
    public bool TryAddItem(ItemBase item, int quantity = 1)
    {
        if (item == null) return false;

        var targetSlots = GetTargetSlot(item);
        if (targetSlots == null)
        {
            Debug.LogWarning("Target 슬롯이 존재하지 않습니다"); return false;
        }
        

        for (int i = 0; i < targetSlots.Count; i++)
        {
            var slot = targetSlots[i];

            if (slot.slotData != null && !slot.slotData.IsEmpty && slot.Accepts(item))
            {
                if (slot.slotData.CanStack(item))
                {
                    slot.slotData.Add(quantity);
                    slot.SetSlot(slot.slotData);
                    return true;
                }
            }
        }

        for (int i = 0; i < targetSlots.Count; i++)
        {
            var slot = targetSlots[i];

            if ((slot.slotData == null || slot.slotData.IsEmpty) && slot.Accepts(item))
            {
                ItemSlot newSlot = new ItemSlot(item, quantity);
                slot.SetSlot(newSlot);
                inventoryData.Add(newSlot);
                return true;
            }
        }

        Debug.Log("인벤토리에 빈 슬롯이 없습니다");
        return false;
    }


    /// <summary>
    /// 슬롯 받아오기
    /// </summary>
    private List<InventorySlot> GetTargetSlot(ItemBase item)
    {
        switch (item.Item_Type)
        {
            case eITEMTYPE.EQUIP:
                if (item is EquipBase equip)
                {
                    switch (equip.equipType)
                    {
                        case eITEMEQUIP_TYPE.WEAPON: return weaponSlots;
                        case eITEMEQUIP_TYPE.ARMOR: return armorSlots;
                        case eITEMEQUIP_TYPE.EARING: return earingSlots;                          
                    }
                }
                break;

            case eITEMTYPE.CONSUMABLE:
                if (item is ConsumableBase consumable)
                {
                    switch (consumable.ConsumableType)
                    {
                        case eITEMCONSUM_TYPE.POTION: return potionSlots;
                        case eITEMCONSUM_TYPE.SCROLL: return scrollSlots;
                        case eITEMCONSUM_TYPE.BOX: return boxSlots;
                    }
                }
                break;

            case eITEMTYPE.ETC:
                return etcSlots;

        }
        return null;
    }
}
