// TableConsumPotionItem.cs
using System;
using System.Collections.Generic;

public class TablePotion : TableBase
{
    [Serializable]
    public class PotionInfo
    {
        public int ID;
        public string Name;
        public string HealTarget; // HP / MP / BOTH
        public float HealPercent;
        public int MaxStack;
        public string Desc;
        public string IconPath;
        public string PrefabPath;
    }

    private Dictionary<int, ConsumablePotion> potionDict = new Dictionary<int, ConsumablePotion>();

    public void Init_CSV(string fileName, int startRow, int startCol)
    {
        CSVReader reader = GetCSVReader(fileName);

        for (int row = startRow; row <= reader.row; row++)
        {
            PotionInfo info = new PotionInfo();
            if (!Read(reader, info, row, startCol))
                break;

            if (!Enum.TryParse(info.HealTarget, out eHEAL_TARGET target))
                target = eHEAL_TARGET.HP;

            ConsumablePotion potion = new ConsumablePotion(
                info.ID, info.Name, info.MaxStack,
                info.Desc, info.IconPath, info.PrefabPath,
                info.HealPercent, target
            );

            potionDict[info.ID] = potion;
        }
    }

    protected bool Read(CSVReader reader, PotionInfo info, int row, int col)
    {
        if (!reader.reset_row(row, col)) return false;

        reader.get(row, ref info.ID);
        reader.get(row, ref info.Name);
        reader.get(row, ref info.HealTarget);
        reader.get(row, ref info.HealPercent);
        reader.get(row, ref info.MaxStack);
        reader.get(row, ref info.Desc);
        reader.get(row, ref info.IconPath);
        reader.get(row, ref info.PrefabPath);

        return true;
    }

    public ConsumablePotion GetItem(int id) => potionDict.TryGetValue(id, out var item) ? item : null;

    public void Save_Binary(string name) => Save_Binary(name, potionDict);
    public void Init_Binary(string name) => Load_Binary(name, ref potionDict);
}
