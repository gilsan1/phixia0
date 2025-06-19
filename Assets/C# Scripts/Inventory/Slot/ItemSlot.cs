using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlot
{
    public ItemBase Item { get; private set; }
    public int Quantity { get; private set; } //수량

    public bool IsEmpty => Item == null || Quantity <= 0; // 슬롯이 비었나 체크
    public bool IsFull => Item != null && Quantity >= Item.maxStack; // 아이템의 최대 수량을 담고 있나 체크

    public ItemSlot(ItemBase item, int quantity)
    {
        Item = item;
        Quantity = quantity;
    }

    public void Add(int amount)
    {
        Quantity = Mathf.Min(Quantity + amount, Item.maxStack);
    }

    public void Remove(int amount)
    {
        Quantity = Mathf.Max(Quantity - amount, 0);
        if (Quantity == 0)
        {
            Clear();
        }
    }
    public void Clear()
    {
        Item = null;
        Quantity = 0;
    }

    public bool CanStack(ItemBase other)
    {
        return Item != null &&
               other != null &&
               Item.Item_Id == other.Item_Id &&
               !IsFull;
    }
}
