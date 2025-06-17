using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory<T>
{
    private Dictionary<int, T> itemDict = new Dictionary<int, T>(); 
    private List<int> keyOrder = new List<int>();

    public void Add(int key, T item)
    {
        if (!itemDict.ContainsKey(key))
        {
            keyOrder.Add(key);
        }

        itemDict[key] = item;
    }

    public void Remove(int key)
    {
        if (itemDict.ContainsKey(key))
        {
            itemDict.Remove(key);
            keyOrder.Remove(key);
        }
    }

    public T Get(int key)
    {
        return itemDict.TryGetValue(key, out var item) ? item : default(T);
    }

    public bool Contains(int key)
    {
        return itemDict.ContainsKey(key);
    }

    public IInventoryIterator<T> GetIterator()
    {
        return new InventoryIterator<T>(itemDict, keyOrder);
    }
}
