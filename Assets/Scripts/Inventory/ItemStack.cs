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

    public void AddToStack(int count) {
        this.count += count;
    }

    public void RemoveFromStack(int count) {
        if (count > this.count) {
            Debug.LogWarning("Attempted to remove more items than currently in the stack.");
            this.count = 0;
        }
        else {
            this.count -= count;
        }
    }
}
