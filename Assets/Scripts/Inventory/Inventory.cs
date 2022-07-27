using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public bool HoldingSeed => selectedStack != null && selectedStack.item is Seed;
    public bool HasFood => stacks.Any(stack => stack.item.Edible);

    [HideInInspector] public List<ItemStack> stacks;
    [HideInInspector] public ItemStack selectedStack;

    public void AddItems(ItemStack stack) {
        var inventoryStack = FindStack(stack.item);
        if (inventoryStack == null)
            // Changes to stacks on ScriptableObjects actually apply
            // to the files themselves, so make a new copy of the stack
            stacks.Add(new ItemStack(stack.item, stack.count));
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

    public ItemStack FindStack(Item item) {
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