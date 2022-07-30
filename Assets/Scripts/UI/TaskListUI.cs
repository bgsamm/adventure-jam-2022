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

    private void Update() {
        if (clock.ActNum == 4) {
            checks[0].enabled = gardenManager.TreeWatered;
        }
        else {
            checks[0].enabled = gardenManager.ShopVisited;
            checks[1].enabled = gardenManager.TreeWatered;
            checks[2].enabled = gardenManager.LetterRead;
            checks[3].enabled = gardenManager.FoodEaten || gardenManager.CantEat;
            if (gardenManager.CantEat)
                checks[3].sprite = taskFailedIcon;
        }
    }
}
