using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject MainMenuPanel;
    //public GameObject OptionsMenuPanel;
    public GameObject CreditsMenuPanel;

    private SceneLoader sceneLoader;
    private CutsceneManager cutsceneManager;
    private LetterManager letterManager;
    private Clock clock;

    private void Start() {
        sceneLoader = ResourceLocator.instance.SceneLoader;
        cutsceneManager = ResourceLocator.instance.CutsceneManager;
        letterManager = ResourceLocator.instance.LetterManager;
        clock = ResourceLocator.instance.Clock;
        ShowMainMenu();
    }

    public void PlayGame() {
        var firstLetter = clock.CurrentDay.letter;
        cutsceneManager.PlayCutscene("Opening",
            delegate {
                letterManager.ShowLetter(firstLetter, sceneLoader.LoadGardenScene);
            });
    }

    private void HideAllSubmenus() {
        MainMenuPanel.SetActive(false);
        //OptionsMenuPanel.SetActive(false);
        CreditsMenuPanel.SetActive(false);
    }

    public void ShowMainMenu() {
        HideAllSubmenus();
        MainMenuPanel.SetActive(true);
    }

    //public void ShowOptionsSubmenu() {
    //    HideAllSubmenus();
    //    OptionsMenuPanel.SetActive(true);
    //}

    public void ShowCreditsSubmenu() {
        HideAllSubmenus();
        CreditsMenuPanel.SetActive(true);
    }
}
