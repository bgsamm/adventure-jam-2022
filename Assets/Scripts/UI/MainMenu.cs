using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject MainMenuPanel;
    //public GameObject OptionsMenuPanel;
    public GameObject CreditsMenuPanel;

    private SceneLoader sceneLoader => ResourceLocator.instance.SceneLoader;
    private CutsceneManager cutsceneManager => ResourceLocator.instance.CutsceneManager;
    private LetterManager letterManager => ResourceLocator.instance.LetterManager;

    private void Start() {
        ShowMainMenu();
    }

    public void PlayGame() {
        var clock = ResourceLocator.instance.Clock;
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
