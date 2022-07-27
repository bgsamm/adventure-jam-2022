using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class GardenUI : MonoBehaviour
{
    [Header("Control Texts")]
    [SerializeField] private TextMeshProUGUI taskListControlText;
    [SerializeField] private TextMeshProUGUI inventoryControlText;
    [SerializeField] private TextMeshProUGUI currentDayText;
    [Header("Panels")]
    [SerializeField] private GameObject taskPanel;
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private GameObject toolbar;

    private Clock clock => ResourceLocator.instance.Clock;

    private void Start() {
        inventoryPanel.SetActive(false);
        taskPanel.SetActive(false);

        currentDayText.text = $"Act {clock.ActNum}, Day {clock.DayNum}";
    }

    private void Update() {
        // Open/close the inventory panel
        if (Input.GetButtonDown("Inventory")) {
            if (!inventoryPanel.activeSelf)
                OpenInventory();
            else
                CloseInventory();
        }
        // Allow 'escape' to close (but not open) the menu
        else if (Input.GetKeyDown(KeyCode.Escape)) {
            CloseInventory();
        }

        // Prevent toggling the task list while player control is disabled
        if (PlayerController.playerHasControl) {
            // Open/close task list
            if (Input.GetButtonDown("Task List")) {
                if (!taskPanel.activeSelf)
                    OpenTaskList();
                else
                    CloseTaskList();
            }
        }
    }

    private void OpenInventory() {
        inventoryPanel.SetActive(true);
        inventoryControlText.text = inventoryControlText.text.Replace("open", "close");
        PlayerController.playerHasControl = false;
        // hide the toolbar when inventory is open to avoid hotkey/scroll conflicts
        toolbar.SetActive(false);
    }

    private void CloseInventory() {
        inventoryPanel.SetActive(false);
        inventoryControlText.text = inventoryControlText.text.Replace("close", "open");
        PlayerController.playerHasControl = true;
        toolbar.SetActive(true);
    }

    private void OpenTaskList() {
        taskPanel.SetActive(true);
        taskListControlText.text = taskListControlText.text.Replace("show", "hide");
    }

    private void CloseTaskList() {
        taskPanel.SetActive(false);
        taskListControlText.text = taskListControlText.text.Replace("hide", "show");
    }
}
