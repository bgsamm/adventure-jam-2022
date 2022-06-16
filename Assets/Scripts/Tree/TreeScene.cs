using UnityEngine;

public class TreeScene : MonoBehaviour
{
    // watered today
    // bird present
    // letter present
    readonly string WATER_CAN = "Watering Can";
    readonly string BIRD = "Bird";
    private bool interacting;

    private void Awake()
    {
        interacting = false;
    }
    public void ClickedObject(string name)
    {
        if (interacting) return;
        interacting = true;

        if(name.Equals(BIRD) && !ResourceLocator.instance.Clock.LetterChecked) {
            if(ResourceLocator.instance.Clock.CurrentDay.birdPresent)
            {
                // Logic for checking the letter
            }
            ResourceLocator.instance.Clock.LetterChecked = true;
        }
        else if (name.Equals(BIRD) && !ResourceLocator.instance.Clock.TreeWatered)
        {
            // Logic for watering the tree
            ResourceLocator.instance.Clock.TreeWatered = true;
        }

        interacting = false;
        Debug.Log(name + " clicked!");
    }
}
