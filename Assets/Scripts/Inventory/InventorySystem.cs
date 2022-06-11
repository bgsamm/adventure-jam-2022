using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem instance { get; private set; }

    public List<ItemStack> stacks;
    [HideInInspector] public ItemStack selectedStack;

    private void Awake() {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else
            instance = this;
    }

    public void AddItem(Item item) {
        int match = stacks.FindIndex(stack => stack.item == item);
        if (match == -1)
            stacks.Add(new ItemStack(item));
        else
            stacks[match].AddItem();
    }

    public void RemoveItem(Item item) {
        int match = stacks.FindIndex(stack => stack.item == item);
        if (match != -1)
            stacks[match].RemoveItem();
    }
}