using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public bool HoldingSeed => selectedStack != null && selectedStack.item is Seed;

    // left exposed in inspector for testing purposes
    public List<ItemStack> stacks;
    [HideInInspector]
    public ItemStack selectedStack;

    public void AddItems(ItemStack stack) {
        var inventoryStack = FindStack(stack.item);
        if (inventoryStack == null)
            stacks.Add(stack);
        else
            inventoryStack.AddToStack(stack.count);
    }

    public void RemoveItems(ItemStack stack) {
        RemoveItems(stack.item, stack.count);
    }

    public void RemoveItems(Item item, int count) {
        var inventoryStack = FindStack(item);
        if (inventoryStack != null) {
            inventoryStack.RemoveFromStack(count);
            if (inventoryStack.count == 0)
                RemoveFromInventory(item);
        }
    }

    public bool HasItems(ItemStack stack) {
        var inventoryStack = FindStack(stack.item);
        return inventoryStack != null && stack.count <= inventoryStack.count;
    }

    private ItemStack FindStack(Item item) {
        int match = stacks.FindIndex(x => x.item == item);
        return match == -1 ? null : stacks[match];
    }

    private void RemoveFromInventory(Item item) {
        int match = stacks.FindIndex(x => x.item == item);
        if (selectedStack == stacks[match])
            selectedStack = null;
        stacks.RemoveAt(match);
    }
}