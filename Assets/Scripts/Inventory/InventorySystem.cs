using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem instance;

    public List<ItemStack> stacks;

    private void Awake() {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else
            instance = this;
    }

    public void AddItem(Item item) {
        int match = stacks.FindIndex(x => x.item == item);
        if (match == -1)
            stacks.Add(new ItemStack(item));
        else
            stacks[match].AddItem();
    }
}