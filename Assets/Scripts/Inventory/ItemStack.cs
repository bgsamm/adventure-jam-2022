using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class ItemStack
{
    // For testing purposes only
    public Item item;
    public int Count;

    //public readonly Item item;
    //public int Count { get; private set; }

    public ItemStack(Item item) {
        this.item = item;
    }

    public void AddItem() {
        Count++;
    }

    public void RemoveItem() {
        if (Count == 0)
            Debug.LogWarning("Attempted to remove item from empty stack");
        else
            Count--;
    }
}
