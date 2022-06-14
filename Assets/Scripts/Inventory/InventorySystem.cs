using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    // left exposed in inspector for testing purposes
    public List<ItemStack> stacks;
    [HideInInspector]
    public ItemStack selectedStack;

    // kept to allow disabling component in editor
    private void Start() { }

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