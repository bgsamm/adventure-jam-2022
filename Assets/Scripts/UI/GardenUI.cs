using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class GardenUI : MonoBehaviour
{
    [Header("Status Texts")]
    [SerializeField] private TextMeshProUGUI currentDayText;
    [SerializeField] private TextMeshProUGUI currentTaskText;
    [Header("Panels")]
    [SerializeField] private GameObject taskPanel;
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private GameObject pauseMenuPanel;

    private Clock clock => ResourceLocator.instance.Clock;
    private GardenManager GardenManager => ResourceLocator.instance.GardenManager;

    private void Start() {
        inventoryPanel.SetActive(false);
        taskPanel.SetActive(false);
        pauseMenuPanel.SetActive(false);

        currentDayText.text = $"Act {clock.ActNum}, Day {clock.DayNum}";
    }

    private void Update() {
        // Open/close pause menu
        if (Input.GetButtonDown("Pause")) {

            if (inventoryPanel.activeSelf)
            {
                inventoryPanel.SetActive(false);
            }
            else
            {
                pauseMenuPanel.SetActive(!pauseMenuPanel.activeSelf);
            }
        }

        if (!pauseMenuPanel.activeSelf) {
            // Open/close the inventory panel
            if (Input.GetButtonDown("Inventory")) {
                inventoryPanel.SetActive(!inventoryPanel.activeSelf);
            }
        }

        bool menuOpen = pauseMenuPanel.activeSelf || inventoryPanel.activeSelf;
        // Hide task list when menus are open
        taskPanel.SetActive(!menuOpen && Input.GetButton("Task List"));
        PlayerController.playerHasControl = !menuOpen;

        currentTaskText.text = GardenManager.CurrentTask;
    }
}
