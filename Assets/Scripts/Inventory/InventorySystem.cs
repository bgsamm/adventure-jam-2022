using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem instance { get; private set; }
    public List<ItemStack> stacks;

    public int selectedSeed { get; private set; }

    private void Awake() {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else
            instance = this;
        selectedSeed = 0;   // Defaults to first in seed bar
    }

    public void AddItem(Item item) {
        int match = stacks.FindIndex(x => x.item == item);
        if (match == -1)
            stacks.Add(new ItemStack(item));
        else
            stacks[match].AddItem();
    }

    public void RemoveItem(Item item)
    {
        int match = stacks.FindIndex(x => x.item == item);
        if (match != -1)
            stacks[match].RemoveItem();
    }
}