using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ============================
// 1. 아이템 데이터 클래스 (순수 데이터)
// ============================
public abstract class ItemBase
{
    public int Item_Id;
    public string Item_Name;
    public string Item_Description;
    public eITEMTYPE Item_Type;


    
    public int maxStack;
    public string ItemIconPath;
    public string ItemPrefabPath;

    public Sprite ItemIcon => LoadIcon();
    public GameObject ItemPrefab => LoadPrefab();
    public bool IsEquip => Item_Type == eITEMTYPE.EQUIP;
    public bool IsComsumable => Item_Type == eITEMTYPE.CONSUMABLE;

    protected ItemBase(int id, string name, eITEMTYPE type, int max, string desc, string iconPath, string prefabPath)
    {
        Item_Id = id;
        Item_Name = name;
        Item_Type = type;
        maxStack = max;
        Item_Description = desc;
        ItemIconPath = iconPath;
        ItemPrefabPath = prefabPath;
    }


    public virtual Sprite LoadIcon()
    {
        return Resources.Load<Sprite>(ItemIconPath);
    }
    
    public virtual GameObject LoadPrefab()
    {
        return Resources.Load<GameObject>(ItemPrefabPath);
    }


    public virtual void EquippedItem(Player player) { }
    public virtual void UseItem(Player player) { }
}
