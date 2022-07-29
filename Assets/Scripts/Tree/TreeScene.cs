using UnityEngine;
using UnityEngine.UI;

public class TreeScene : MonoBehaviour
{
    [SerializeField] private Image background;
    [SerializeField] private Bird bird;
    [SerializeField] private WateringCan wateringCan;
    [SerializeField] GameObject acorns;

    private Clock clock => ResourceLocator.instance.Clock;
    private GardenManager gardenManager => ResourceLocator.instance.GardenManager;
    private LetterManager letterManager => ResourceLocator.instance.LetterManager;
    private SceneLoader sceneLoader => ResourceLocator.instance.SceneLoader;
    private UnityAudioManager audioManager => ResourceLocator.instance.SFXManager;

    private void Start() {
        if (gardenManager.TreeWatered)
            background.sprite = clock.CurrentAct.treePortraitWatered;
        else
            background.sprite = clock.CurrentAct.treePortrait;

        // if there is no letter, simply checking the tree
        // satisfies the "Check for letters" task
        if (clock.CurrentDay.letter == null)
            gardenManager.LetterChecked = true;
        bird.gameObject.SetActive(clock.CurrentDay.birdPresent);

        if (clock.ActNum == 3 && clock.DayNum == 7)
            acorns.SetActive(true);
        else
            acorns.SetActive(false);

        // if bird is present and letter is unread, plays birdsong
        if (clock.CurrentDay.birdPresent && !gardenManager.LetterChecked)
        {
            audioManager.PlayOneShot(audioManager.birdsong);
        }
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
