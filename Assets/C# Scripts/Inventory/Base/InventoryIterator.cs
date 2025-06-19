using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryIterator<T> : IInventoryIterator<T>
{
    private readonly Dictionary<int, T> ItemDict;
    private readonly List<int> KeyList;

    private int position = 0;
    private int key;

    public InventoryIterator(Dictionary<int, T> itemDict, List<int> keyList)
    {
        ItemDict = itemDict;
        KeyList = keyList;
    }

    public bool HasNext()
    {
        return position < KeyList.Count;
    }

    public T Next()
    {
        if (!HasNext()) return default;

        key = KeyList[position++];
        return ItemDict[key];
    }
}
