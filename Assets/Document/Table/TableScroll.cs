using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableScroll : TableBase
{
    [Serializable]
    public class ScrollInfo
    {
        public int ID;
        public string Name;
        public string BuffType; // ATK_UP / DEF_UP
        public float BuffAmount;
        public float Duration;
        public int MaxStack;
        public string Desc;
        public string IconPath;
        public string PrefabPath;
    }

    private Dictionary<int, ConsumableScroll> scrollDict = new Dictionary<int, ConsumableScroll>();

    public void Init_CSV(string fileName, int startRow, int startCol)
    {
        CSVReader reader = GetCSVReader(fileName);

        for (int row = startRow; row <= reader.row; row++)
        {
            ScrollInfo info = new ScrollInfo();
            if (!Read(reader, info, row, startCol))
                break;

            if (!Enum.TryParse(info.BuffType, out eBUFF_TYPE type))
                type = eBUFF_TYPE.NONE;

            ConsumableScroll scroll = new ConsumableScroll(
                info.ID, info.Name, info.MaxStack,
                info.Desc, info.IconPath, info.PrefabPath,
                type, info.BuffAmount, info.Duration
            );

            scrollDict[info.ID] = scroll;
        }
    }

    protected bool Read(CSVReader reader, ScrollInfo info, int row, int col)
    {
        if (!reader.reset_row(row, col)) return false;

        reader.get(row, ref info.ID);
        reader.get(row, ref info.Name);
        reader.get(row, ref info.BuffType);
        reader.get(row, ref info.BuffAmount);
        reader.get(row, ref info.Duration);
        reader.get(row, ref info.MaxStack);
        reader.get(row, ref info.Desc);
        reader.get(row, ref info.IconPath);
        reader.get(row, ref info.PrefabPath);

        return true;
    }

    public ConsumableScroll GetItem(int id) => scrollDict.TryGetValue(id, out var item) ? item : null;

    public void Save_Binary(string name) => Save_Binary(name, scrollDict);
    public void Init_Binary(string name) => Load_Binary(name, ref scrollDict);
}
