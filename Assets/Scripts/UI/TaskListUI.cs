using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaskListUI : MonoBehaviour
{
    [SerializeField] private Sprite taskFailedIcon;
    [SerializeField] private Image[] checks = new Image[4];

    private Clock clock => ResourceLocator.instance.Clock;
    private GardenManager gardenManager => ResourceLocator.instance.GardenManager;
    private Inventory inventory => ResourceLocator.instance.InventorySystem;

    private void Update() {
        if (clock.ActNum == 4) {
            checks[0].enabled = gardenManager.TreeWatered;
        }
        else {
            checks[0].enabled = gardenManager.ShopVisited;
            checks[1].enabled = gardenManager.TreeWatered;
            checks[2].enabled = gardenManager.LetterChecked;
            checks[3].enabled = gardenManager.FoodEaten || !inventory.HasFood;
            if (!inventory.HasFood && !gardenManager.FoodEaten)
                checks[3].sprite = taskFailedIcon;
        }
    }
}
