using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIManager : MonoBehaviour
{
    [Header("EQUIPMENT Trasnform")]
    public Transform panel_Weapon;
    public Transform panel_Armor;
    public Transform panel_Earing;

    [Header("CONSUMABLE Transform")]
    public Transform panel_Potion;
    public Transform panel_Scroll;
    public Transform panel_Box;

    [Header("SLOT LIST")]
    public List<InventorySlot> weapon_Slots;
    public List<InventorySlot> armor_Slots;
    public List<InventorySlot> earing_Slots;

    public List<InventorySlot> potion_Slots;
    public List<InventorySlot> scroll_slots;
    public List<InventorySlot> box_slots;


}
