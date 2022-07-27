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
    [SerializeField] private GameObject toolbar;
    [Header("Item Description")]
    [SerializeField] private TextMeshProUGUI NameText;
    [SerializeField] private TextMeshProUGUI DescriptionText;

    private InventorySlot[] inventorySlots;
    private int selectionIndex => Array.FindIndex(inventorySlots, x => x.Selected);
    private InventorySlot selectedSlot => selectionIndex >= 0 ? inventorySlots[selectionIndex] : null;

    private InventorySystem inventory => ResourceLocator.instance.InventorySystem;
    private GardenManager gardenManager => ResourceLocator.instance.GardenManager;

    private void Start() {
        inventorySlots = inventoryPanel.GetComponentsInChildren<InventorySlot>();
        inventoryPanel.SetActive(false);

        NameText.text = "";
        DescriptionText.text = "Click an item to view its description";
    }

    private void Update() {
        // Open/close the inventory panel
        if (Input.GetButtonDown("Inventory")) {
            if (!inventoryPanel.activeSelf)
                OpenPanel();
            else
                ClosePanel();
        }
        // Allow 'escape' to close (but not open) the menu
        else if (Input.GetKeyDown(KeyCode.Escape)) {
            ClosePanel();
        }

        if (inventoryPanel.activeSelf) {
            // Allow scroll wheel to change selected item
            if (Input.mouseScrollDelta.y != 0) {
                if (Input.mouseScrollDelta.y < 0 && selectionIndex < inventorySlots.Length - 1) {
                    SelectSlot(selectionIndex + 1);
                }
                else if (Input.mouseScrollDelta.y > 0 && selectionIndex > 0) {
                    SelectSlot(selectionIndex - 1);
                }
            }
            // Eating
            if (Input.GetKeyDown(KeyCode.E) && selectionIndex >= 0) {
                if (selectedSlot != null && selectedSlot.Stack != null)
                    EatItem(selectedSlot.Stack.item);
            }
        }

        // Update item description
        if (selectedSlot != null && selectedSlot.Stack != null) {
            var item = selectedSlot.Stack.item;
            NameText.text = item.name;
            DescriptionText.text = item.Description;
            if (item.Edible)
                DescriptionText.text += " Press E to eat.";
        }
        else {
            NameText.text = "";
            DescriptionText.text = "";
        }
    }

    private void OpenPanel() {
        UpdateSlots();
        inventoryPanel.SetActive(true);
        controlText.text = controlText.text.Replace("open", "close");
        PlayerController.playerHasControl = false;
        // hide the toolbar when inventory is open to avoid hotkey/scroll conflicts
        toolbar.SetActive(false);
    }

    private void ClosePanel() {
        inventoryPanel.SetActive(false);
        controlText.text = controlText.text.Replace("close", "open");
        PlayerController.playerHasControl = true;
        toolbar.SetActive(true);
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

    private void SelectSlot(int index) {
        var slot = inventorySlots[index];
        SelectSlot(slot);
    }

    public void SelectSlot(InventorySlot slot) {
        // Select provided slot, deselect all others
        foreach (var inventorySlot in inventorySlots) {
            inventorySlot.SetSelected(inventorySlot == slot);
        }
    }

    public void EatItem(Item food) {
        if (food.Edible) {
            inventory.RemoveItems(food, 1);
            UpdateSlots();
            gardenManager.FoodEaten = true;
            UpdateSlots();
        }
    }
}
