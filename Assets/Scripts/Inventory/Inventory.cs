using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI controlText;
    [SerializeField] private GameObject inventoryPanel;
    private InventorySlot[] inventorySlots;

    private InventorySystem inventory => ResourceLocator.instance.InventorySystem;

    private void Start() {
        inventorySlots = inventoryPanel.GetComponentsInChildren<InventorySlot>();
        inventoryPanel.SetActive(false);
    }

    private void Update() {
        if (inventoryPanel.activeSelf)
            PlayerController.playerHasControl = false;

        if (Input.GetButtonDown("Inventory")) {
            string text;
            if (!inventoryPanel.activeSelf) {
                UpdateSlots();
                inventoryPanel.SetActive(true);
                text = controlText.text.Replace("open", "close");
            }
            else {
                inventoryPanel.SetActive(false);
                text = controlText.text.Replace("close", "open");
                PlayerController.playerHasControl = true;
            }
            controlText.text = text;
        }
        // Allow 'escape' to close (but not open) the menu
        else if (Input.GetKeyDown(KeyCode.Escape)) {
            inventoryPanel.SetActive(false);
            controlText.text = controlText.text.Replace("close", "open");
            PlayerController.playerHasControl = true;
        }
    }

    private void UpdateSlots() {
        for (int i = 0; i < inventorySlots.Length; i++) {
            var inventorySlot = inventorySlots[i];
            if (i < inventory.stacks.Count)
                inventorySlot.SetStack(inventory.stacks[i]);
            else
                inventorySlot.SetStack(null);
        }
    }
}
