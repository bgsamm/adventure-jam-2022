using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject MainMenuPanel;
    //public GameObject OptionsMenuPanel;
    public GameObject CreditsMenuPanel;

    private void Start() {
        ShowMainMenu();
    }

    public void PlayGame() {
        ResourceLocator.instance.Clock.PlayFromStart();
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
