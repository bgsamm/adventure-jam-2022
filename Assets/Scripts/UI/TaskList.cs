using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaskList : MonoBehaviour
{
    [SerializeField] private Image[] checks = new Image[4];

    private GardenManager gardenManager => ResourceLocator.instance.GardenManager;
    private Clock clock => ResourceLocator.instance.Clock;

    private void Update() {
        if (clock.ActNum == 4) {
            checks[0].enabled = gardenManager.TreeWatered;
        }
        else {
            checks[0].enabled = gardenManager.ShopVisited;
            checks[1].enabled = gardenManager.TreeWatered;
            checks[2].enabled = gardenManager.LetterChecked;
            checks[3].enabled = gardenManager.FoodEaten;
        }
    }
}
