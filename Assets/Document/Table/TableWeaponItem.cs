// TableWeaponItem.cs
using System;
using System.Collections.Generic;

public class TableWeaponItem : TableBase
{
    [Serializable]
    public class WeaponInfo
    {
        public int ID;
        public string Name;
        public string Type;          // EQUIP
        public string EquipType;     // WEAPON
        public float Atk;
        public float Def;
        public string Desc;
        public int MaxStack;
        public string IconPath;
        public string PrefabPath;
    }

    private Dictionary<int, EquipWeapon> weaponDict = new Dictionary<int, EquipWeapon>();

    public void Init_CSV(string fileName, int startRow, int startCol)
    {
        CSVReader reader = GetCSVReader(fileName);

        for (int row = startRow; row <= reader.row; row++)
        {
            WeaponInfo info = new WeaponInfo();
            if (!Read(reader, info, row, startCol))
                break;

            EquipWeapon weapon = new EquipWeapon(
                info.ID, info.Name, eITEMTYPE.EQUIP, info.MaxStack,
                info.Desc, info.IconPath, info.PrefabPath,
                info.Atk, info.Def, eITEMEQUIP_TYPE.WEAPON);

            weaponDict[info.ID] = weapon;
        }
    }

    protected bool Read(CSVReader reader, WeaponInfo info, int row, int col)
    {
        if (!reader.reset_row(row, col)) return false;

        reader.get(row, ref info.ID);
        reader.get(row, ref info.Name);
        reader.get(row, ref info.Type);
        reader.get(row, ref info.EquipType);
        reader.get(row, ref info.Atk);
        reader.get(row, ref info.Def);
        reader.get(row, ref info.Desc);
        reader.get(row, ref info.MaxStack);
        reader.get(row, ref info.IconPath);
        reader.get(row, ref info.PrefabPath);

        return true;
    }

    public EquipWeapon GetItem(int id) => weaponDict.TryGetValue(id, out var item) ? item : null;

    public void Save_Binary(string name) => Save_Binary(name, weaponDict);
    public void Init_Binary(string name) => Load_Binary(name, ref weaponDict);
}
