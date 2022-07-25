using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Toolbar : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI selectedItemLabel;

    private InventorySlot[] slots;
    private int activeSlot;

    private InventorySystem inventory => ResourceLocator.instance.InventorySystem;

    private void Start() {
        slots = GetComponentsInChildren<InventorySlot>();
        activeSlot = 0;
        UpdateSlots();
    }

    private void Update() {
        // loops negative values around correctly
        static int mod(int x, int m) {
            return (x % m + m) % m;
        }
        // I don't love calling this every frame but it certainly is the simplest approach
        UpdateSlots();
        // Map inputs Toolbar 1..N to the corresponding toolbar slots
        for (int i = 0; i < slots.Length; i++) {
            if (Input.GetButtonDown($"Toolbar {i + 1}")) {
                activeSlot = i;
                break;
            }
        }
        // Allow scroll wheel to change selected item
        if (Input.mouseScrollDelta.y < 0)
            activeSlot = mod(activeSlot + 1, slots.Length);
        else if (Input.mouseScrollDelta.y > 0)
            activeSlot = mod(activeSlot - 1, slots.Length);
        // Also don't love calling this every frame
        SelectSlot(activeSlot);
    }

    private void SelectSlot(int index) {
        for (int i = 0; i < slots.Length; i++) {
            slots[i].SetSelected(i == index);
        }
        var stack = slots[index].Stack;
        inventory.selectedStack = stack;
        // update selected item text
        selectedItemLabel.text = stack != null ? stack.item.name : "";
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
