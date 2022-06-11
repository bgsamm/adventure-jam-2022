using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Toolbar : MonoBehaviour
{
    private InventorySlot[] slots;

    private void Start() {
        slots = GetComponentsInChildren<InventorySlot>();
        SelectSlot(0);
    }

    private void Update() {
        // Toolbar 1..N
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
    }
}
