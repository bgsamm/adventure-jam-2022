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

    public ItemStack(Item item, int count) {
        this.item = item;
        this.count = count;
    }

    /// <summary>
    /// InventorySystem.AddItems should typically be used rather than calling this method directly.
    /// </summary>
    public void AddToStack(int count) {
        this.count += count;
    }

    /// <summary>
    /// InventorySystem.RemoveItems should typically be used rather than calling this method directly.
    /// </summary>
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
