using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class ItemStack
{
    public Item item;
    public int count;

    public ItemStack(Item item) {
        this.item = item;
    }

    public void AddItem() {
        count++;
    }

    public void RemoveItem() {
        if (count == 0)
            Debug.LogWarning("Attempted to remove item from empty stack");
        else
            count--;
    }
}
