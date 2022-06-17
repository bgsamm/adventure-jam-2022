using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [Header("Submenus")]
    [SerializeField] private GameObject MainMenuPanel;
    //[SerializeField] private GameObject OptionsMenuPanel;
    [SerializeField] private GameObject CreditsMenuPanel;
    [Header("Audio")]
    [SerializeField] private AudioClip menuMusic;

    private UnityAudioManager audioManager => ResourceLocator.instance.AudioManager;
    private Clock clock => ResourceLocator.instance.Clock;

    private void Start() {
        if (clock.ActNum == 5)
            ShowCreditsSubmenu();
        else
            ShowMainMenu();

        audioManager.PlayLoop(menuMusic);
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
