using UnityEngine;
using UnityEngine.UI;

public class TreeScene : MonoBehaviour
{
    [SerializeField] private Image background;
    [SerializeField] private Bird bird;
    [SerializeField] private WateringCan wateringCan;

    private Clock clock => ResourceLocator.instance.Clock;
    private GardenManager gardenManager => ResourceLocator.instance.GardenManager;
    private LetterManager letterManager => ResourceLocator.instance.LetterManager;
    private SceneLoader sceneLoader => ResourceLocator.instance.SceneLoader;

    private void Start() {
        if (gardenManager.TreeWatered)
            background.sprite = clock.CurrentAct.treePortraitWatered;
        else
            background.sprite = clock.CurrentAct.treePortrait;

        bird.gameObject.SetActive(clock.CurrentDay.birdPresent);
    }

    public void ReadLetter() {
        var letter = clock.CurrentDay.letter;
        if (letter != null & !gardenManager.LetterChecked) {
            gardenManager.LetterChecked = true;
            letterManager.ShowLetter(clock.CurrentDay.letter, sceneLoader.LoadTreeScene);
        }
    }

    public void WaterTree() {
        Debug.Log("You watered the tree!");
        wateringCan.PlayWateringAnim(
            delegate {
                background.sprite = clock.CurrentAct.treePortraitWatered;
                gardenManager.TreeWatered = true;
        });
    }

    public void ReturnToGarden() {
        sceneLoader.LoadGardenScene();
    }
}
