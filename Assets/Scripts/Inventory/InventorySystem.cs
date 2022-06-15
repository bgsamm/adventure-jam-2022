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

    public void AddItems(ItemStack stack) {
        var inventoryStack = FindStack(stack.item);
        if (inventoryStack == null)
            stacks.Add(stack);
        else
            inventoryStack.AddToStack(stack.count);
    }

    public void RemoveItems(ItemStack stack) {
        var inventoryStack = FindStack(stack.item);
        if (inventoryStack != null)
            inventoryStack.RemoveFromStack(stack.count);
    }

    public bool HasItems(ItemStack stack) {
        var inventoryStack = FindStack(stack.item);
        return inventoryStack != null && stack.count <= inventoryStack.count;
    }

    private ItemStack FindStack(Item item) {
        int match = stacks.FindIndex(x => x.item == item);
        return match == -1 ? null : stacks[match];
    }
}