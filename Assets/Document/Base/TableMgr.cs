
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class TableMgr
{
    public static TableMgr Instance { get; private set; }

    //public TableCharacter Character = new TableCharacter();

    /// <summary>
    /// 아이템 관련 테이블 
    /// </summary>
    public TableWeaponItem weaponItem = new TableWeaponItem();
    public TableArmorItem armorItem = new TableArmorItem();
    public TablePotion potionItem = new TablePotion();
    public TableScroll scrollItem = new TableScroll();

    /// <summary>
    /// 퀘스트 
    /// </summary>
    public TableQuest questData = new TableQuest();


    /// <summary>
    /// 스킬 
    /// </summary>
    public TableSkill Skill = new TableSkill();

    public TableMgr()
    {
        Instance = this;
        Init();
    }

    
    public void Init()
    {
#if UNITY_EDITOR
        //Character.Init_CSV("Character", 1, 0);


        weaponItem.Init_CSV("Items_Weapon", 1, 0);
        armorItem.Init_CSV("Items_Armor", 1, 0);
        potionItem.Init_CSV("Items_ConsumPotion", 1, 0);
        scrollItem.Init_CSV("Items_ConsumScroll", 1, 0);


        questData.Init_CSV("Quests", 1, 0);
        Skill.Init_CSV("SKills", 1, 0);
#else
        Character.Init_Binary("Character");
        Item.Init_Binary("DropItem");
#endif
    }

    public void Save()
    {
        //Character.Save_Binary("Character");

        weaponItem.Save_Binary("Items_Weapon");
        armorItem.Save_Binary("Items_Armor");
        potionItem.Save_Binary("Items_ConsumPotion");
        scrollItem.Save_Binary("Items_ConsumScroll");
        questData.Save_Binary("Quests");
        Skill.Save_Binary("Skills");

#if UNITY_EDITOR
        AssetDatabase.Refresh();
#endif
    }
}
