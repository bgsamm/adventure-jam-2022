using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Toolbar : MonoBehaviour
{
    public InventorySlot[] slots;

    private void Start() {
        slots = GetComponentsInChildren<InventorySlot>();
        UpdateSlots();
        SelectSlot(0);
    }

    private void Update() {
        // Map inputs Toolbar 1..N to the corresponding toolbar slots
        for (int i = 0; i < slots.Length; i++) {
            if (Input.GetButtonDown($"Toolbar {i + 1}")) {
                SelectSlot(i);
                break;
            }
        }
    }

    private void SelectSlot(int index) {
        for (int i = 0; i < slots.Length; i++) {
            slots[i].SetSelected(i == index);
        }
        InventorySystem.instance.selectedStack = slots[index].Stack;
    }

    private void UpdateSlots() {
        for (int i = 0; i < slots.Length; i++) {
            slots[i].SetStack(null);
        }
    }
}
