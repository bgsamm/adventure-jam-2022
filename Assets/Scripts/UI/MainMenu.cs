using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject MainMenuPanel;
    public GameObject OptionsMenuPanel;
    public GameObject CreditsMenuPanel;

    private SceneLoader sceneLoader;
    private CutsceneManager cutsceneManager;

    private void Start() {
        sceneLoader = ResourceLocator.instance.SceneLoader;
        cutsceneManager = ResourceLocator.instance.CutsceneManager;
        ShowMainMenu();
    }

    public void PlayGame() {
        cutsceneManager.PlayCutscene("Opening", delegate { sceneLoader.LoadGardenScene(); });
    }

    private void HideAllSubmenus() {
        MainMenuPanel.SetActive(false);
        OptionsMenuPanel.SetActive(false);
        CreditsMenuPanel.SetActive(false);
    }

    public void ShowMainMenu() {
        HideAllSubmenus();
        MainMenuPanel.SetActive(true);
    }

    public void ShowOptionsSubmenu() {
        HideAllSubmenus();
        OptionsMenuPanel.SetActive(true);
    }

    public void ShowCreditsSubmenu() {
        HideAllSubmenus();
        CreditsMenuPanel.SetActive(true);
    }
}
