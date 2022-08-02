using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [Header("Item Description")]
    [SerializeField] private TextMeshProUGUI NameText;
    [SerializeField] private TextMeshProUGUI DescriptionText;

    private InventorySlot[] inventorySlots;
    private int selectionIndex => Array.FindIndex(inventorySlots, x => x.Selected);
    private InventorySlot selectedSlot => inventorySlots[selectionIndex];

    private Inventory inventory => ResourceLocator.instance.InventorySystem;
    private GardenManager gardenManager => ResourceLocator.instance.GardenManager;

    private void Start() {
        inventorySlots = GetComponentsInChildren<InventorySlot>();
        UpdateSlots();
        SelectSlot(inventorySlots[0]);


        NameText.text = "";
        DescriptionText.text = "Click an item to view its description";
    }

    private void Update() {
        UpdateSlots();
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
        if (Input.GetButtonDown("Eat") && selectionIndex >= 0) {
            if (selectedSlot.Stack != null)
                EatItem(selectedSlot.Stack.item);
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

    private void SelectSlot(int index) {
        var slot = inventorySlots[index];
        SelectSlot(slot);
    }

    public void SelectSlot(InventorySlot slot) {
        // Select provided slot, deselect all others
        foreach (var inventorySlot in inventorySlots) {
            inventorySlot.SetSelected(inventorySlot == slot);
        }
        // Update description text
        if (selectedSlot.Stack != null) {
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

    public void EatItem(Item food) {
        NameText.text = "";
        if (!food.Edible) {
            DescriptionText.text = "You can't eat that!";
        }
        else if (gardenManager.FoodEaten) {
            DescriptionText.text = "You're too full to eat any more.";
        }
        else {
            inventory.RemoveItems(food, 1);
            UpdateSlots();
            gardenManager.FoodEaten = true;
            DescriptionText.text = "Mm, tasty.";
        }
    }
}
