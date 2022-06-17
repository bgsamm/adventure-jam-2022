using UnityEngine;

public class TreeScene : MonoBehaviour
{
    [Header("Bird")]
    [SerializeField] SpriteRenderer bird;
    [SerializeField] Sprite birdWithLetter;
    [SerializeField] Sprite birdWithoutLetter;

    [Header("Watering Can")]
    [SerializeField] SpriteRenderer wateringCan;
    // watered today
    // bird present
    // letter present
    readonly string WATER_CAN = "Watering Can";
    readonly string BIRD = "Bird";
    private bool interacting;

    private Clock clock => ResourceLocator.instance.Clock;
    private GardenManager gardenManager => ResourceLocator.instance.GardenManager;
    private LetterManager letterManager => ResourceLocator.instance.LetterManager;
    private SceneLoader scene => ResourceLocator.instance.SceneLoader;

    private void Awake() {
        interacting = false;
        
        if (clock.CurrentDay.birdPresent)
        {
            if (clock.CurrentDay.letter != null && !gardenManager.LetterChecked)
                bird.sprite = birdWithLetter;
            else
                bird.sprite = birdWithoutLetter;
        }
        else
        {
            bird.enabled = false;
            gardenManager.LetterChecked = true;
        }
    }

    public void ClickedObject(GameObject obj) {
        if (interacting) return;
        interacting = true;

        if (obj.name.Equals(BIRD) && !gardenManager.LetterChecked) {
            if (clock.CurrentDay.birdPresent && clock.CurrentDay.letter != null) {
                letterManager.ShowLetter(clock.CurrentDay.letter, scene.LoadTreeScene);
            }
            gardenManager.LetterChecked = true;
        }
        else if (name.Equals(WATER_CAN) && !gardenManager.TreeWatered) {
            gardenManager.TreeWatered = true;
        }

        obj.GetComponent<Clicking>().enabled = false;  // makes click script stop
        interacting = false;
        Debug.Log(name + " clicked!");
    }
}
