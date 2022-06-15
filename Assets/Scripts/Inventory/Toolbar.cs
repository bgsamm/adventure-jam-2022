using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Toolbar : MonoBehaviour
{
    public InventorySlot[] slots;

    private InventorySystem inventory => ResourceLocator.instance.InventorySystem;

    private void Start() {
        slots = GetComponentsInChildren<InventorySlot>();
        UpdateSlots();
        SelectSlot(0);
    }

    private void Update() {
        UpdateSlots();
        // Map inputs Toolbar 1..N to the corresponding toolbar slots
        for (int i = 0; i < slots.Length; i++) {
            if (Input.GetButtonDown($"Toolbar {i + 1}"))
                SelectSlot(i);
        }
    }

    private void SelectSlot(int index) {
        for (int i = 0; i < slots.Length; i++) {
            slots[i].SetSelected(i == index);
        }
        inventory.selectedStack = slots[index].Stack;
    }

    private void UpdateSlots() {
        var seeds = inventory.stacks.Where(stack => stack.item is Seed).ToArray();
        for (int i = 0; i < slots.Length; i++) {
            if (i < seeds.Length)
                slots[i].SetStack(seeds[i]);
            else
                slots[i].SetStack(null);
        }
    }
}
