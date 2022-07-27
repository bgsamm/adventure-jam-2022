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

    [SerializeField] private TextMeshProUGUI NameText;
    [SerializeField] private TextMeshProUGUI DescriptionText;

    [SerializeField] private GameObject toolbar;

    private int selectionIndex = 0;

    private InventorySystem inventory => ResourceLocator.instance.InventorySystem;

    private GardenManager gardenManager => ResourceLocator.instance.GardenManager;

    private void Start() {
        inventorySlots = inventoryPanel.GetComponentsInChildren<InventorySlot>();
        inventoryPanel.SetActive(false);
    }

    private void Update() {
        if (inventoryPanel.activeSelf)
        {
            PlayerController.playerHasControl = false;

            // Allow scroll wheel to change selected item
            if (Input.mouseScrollDelta.y < 0 && selectionIndex < inventory.stacks.Count - 1)
                selectionIndex = selectionIndex + 1;
            else if (Input.mouseScrollDelta.y > 0 && selectionIndex > 0)
                selectionIndex = selectionIndex - 1;
            // Also don't love calling this every frame
            SelectSlot(selectionIndex);

            if (Input.GetKeyDown(KeyCode.E))
            {
                Eat(inventory.stacks[selectionIndex]);
            }
        }

        if (Input.GetButtonDown("Inventory")) {
            string text;
            if (!inventoryPanel.activeSelf) {
                UpdateSlots();
                inventoryPanel.SetActive(true);
                text = controlText.text.Replace("open", "close");

                //hiding the toolbar when inventory is open--to avoid hotkey/scroll conflicts
                toolbar.SetActive(false);
            }
            else {
                inventoryPanel.SetActive(false);
                text = controlText.text.Replace("close", "open");
                PlayerController.playerHasControl = true;
                toolbar.SetActive(true);
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

    private void SelectSlot(int index)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            inventorySlots[i].SetSelected(i == index);
        }
        var stack = inventorySlots[index].Stack;
        if (stack != null)
        {
            inventory.selectedStack = stack;
            ExamineItem(stack.item);
        }
    }

    public void ExamineItem(Item item)
    {
        NameText.text = item.name;
        DescriptionText.text = item.Description;

        if (item.Edible)
            DescriptionText.text = DescriptionText.text + " Press E to eat.";
    }

    public void Eat(ItemStack food)
    {
        if (food.item.Edible)
        {
            food.RemoveFromStack(1);
            gardenManager.FoodEaten = true;
            UpdateSlots();
        }
    }
}
