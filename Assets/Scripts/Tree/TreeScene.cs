using UnityEngine;

public class TreeScene : MonoBehaviour
{
    // watered today
    // bird present
    // letter present
    readonly string WATER_CAN = "Watering Can";
    readonly string BIRD = "Bird";
    private bool interacting;

    private Clock clock => ResourceLocator.instance.Clock;
    private GardenManager gardenManager => ResourceLocator.instance.GardenManager;

    private void Awake() {
        interacting = false;
    }

    public void ClickedObject(string name) {
        if (interacting) return;
        interacting = true;

        if (name.Equals(BIRD) && !gardenManager.LetterChecked) {
            if (clock.CurrentDay.birdPresent) {
                // Logic for checking the letter
            }
            gardenManager.LetterChecked = true;
        }
        else if (name.Equals(WATER_CAN) && !gardenManager.TreeWatered) {
            // Logic for watering the tree
            gardenManager.TreeWatered = true;
        }

        interacting = false;
        Debug.Log(name + " clicked!");
    }
}
