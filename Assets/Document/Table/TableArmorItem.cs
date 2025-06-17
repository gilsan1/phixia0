// TableArmorItem.cs
using System;
using System.Collections.Generic;

public class TableArmorItem : TableBase
{
    [Serializable]
    public class ArmorInfo
    {
        public int ID;
        public string Name;
        public string Type;          // EQUIP
        public string EquipType;     // ARMOR
        public string ArmorType;     // HELMET, CHEST µî
        public float Atk;
        public float Def;
        public string Desc;
        public int MaxStack;
        public string IconPath;
        public string PrefabPath;
    }

    private Dictionary<int, EquipArmor> armorDict = new Dictionary<int, EquipArmor>();

    public void Init_CSV(string fileName, int startRow, int startCol)
    {
        CSVReader reader = GetCSVReader(fileName);

        for (int row = startRow; row <= reader.row; row++)
        {
            ArmorInfo info = new ArmorInfo();
            if (!Read(reader, info, row, startCol))
                break;

            if (!Enum.TryParse(info.ArmorType, out eARMORTYPE armorType))
                armorType = eARMORTYPE.NONE;

            EquipArmor armor = new EquipArmor(
                info.ID, info.Name, eITEMTYPE.EQUIP, info.MaxStack,
                info.Desc, info.IconPath, info.PrefabPath,
                info.Atk, info.Def, eITEMEQUIP_TYPE.ARMOR, armorType);

            armorDict[info.ID] = armor;
        }
    }

    protected bool Read(CSVReader reader, ArmorInfo info, int row, int col)
    {
        if (!reader.reset_row(row, col)) return false;

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

    public EquipArmor GetItem(int id) => armorDict.TryGetValue(id, out var item) ? item : null;

    public void Save_Binary(string name) => Save_Binary(name, armorDict);
    public void Init_Binary(string name) => Load_Binary(name, ref armorDict);
}