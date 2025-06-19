using System.Collections.Generic;
using System;
using static UnityEditor.Progress;
using Unity.VisualScripting;

public class TableItem : TableBase
{
    [Serializable]
    public class ItemInfo
    {
        public int ID;
        public string Name;
        public string Type;          // EQUIP / CONSUMABLE / ETC
        public string EquipType;     // WEAPON / ARMOR 등
        public string ArmorType;     // HELMET 등 (방어구 전용)
        public string ConsumType;    // HEALHP 등 (소비아이템 전용)
        public float Atk;
        public float Def;
        public string Desc;
        public int MaxStack;
        public string IconPath;
        public string PrefabPath;
    }

    private Dictionary<int, ItemBase> itemDict = new Dictionary<int, ItemBase>();


    public void Init_CSV(string fileName, int startRow, int startCol)
    {
        CSVReader reader = GetCSVReader(fileName);

        for (int row = startRow; row <= reader.row; row++)
        {
            ItemInfo info = new ItemInfo();
            if (!Read(reader, info, row, startCol))
                break;

            // info를 기반으로 실제 ItemBase 객체 생성
            ItemBase item = null;

            if (!Enum.TryParse(info.Type, out eITEMTYPE type)) continue;
            if (!Enum.TryParse(info.EquipType, out eITEMEQUIP_TYPE equipType)) equipType = eITEMEQUIP_TYPE.NONE;
            if (!Enum.TryParse(info.ArmorType, out eARMORTYPE armorType)) armorType = eARMORTYPE.NONE;

            switch (type)
            {
                case eITEMTYPE.EQUIP:
                    if (equipType == eITEMEQUIP_TYPE.WEAPON)
                    {
                        item = new EquipWeapon(
                            info.ID, info.Name, type, info.MaxStack, 
                            info.Desc, info.IconPath, info.PrefabPath, 
                            info.Atk, info.Def, equipType);
                    }
                    else if (equipType == eITEMEQUIP_TYPE.ARMOR)
                    {
                        item = new EquipArmor(
                            info.ID, info.Name, type, info.MaxStack,
                            info.Desc, info.IconPath, info.PrefabPath,
                            info.Atk, info.Def, equipType, armorType);
                    }
                    break;
            }

            if (item != null)
                itemDict[info.ID] = item;
        }
    }

    protected bool Read(CSVReader reader, ItemInfo info, int row, int col)
    {
        if (!reader.reset_row(row, col))
            return false;

        reader.get(row, ref info.ID);
        reader.get(row, ref info.Name);
        reader.get(row, ref info.Type);
        reader.get(row, ref info.EquipType);
        reader.get(row, ref info.ArmorType);
        reader.get(row, ref info.Atk);
        reader.get(row, ref info.Def);
        reader.get(row, ref info.Desc);
        reader.get(row, ref info.MaxStack);
        reader.get(row, ref info.IconPath);
        reader.get(row, ref info.PrefabPath);

        return true;
    }

    public ItemBase GetItem(int id)
    {
        return itemDict.TryGetValue(id, out var item) ? item : null;
    }

    public void Save_Binary(string name)
    {
        Save_Binary(name, itemDict);
    }

    public void Init_Binary(string name)
    {
        Load_Binary(name, ref itemDict);
    }
}