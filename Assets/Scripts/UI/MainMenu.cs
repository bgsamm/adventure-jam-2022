using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [Header("Submenus")]
    [SerializeField] private GameObject MainMenuPanel;
    [SerializeField] private GameObject CreditsMenuPanel;
    [SerializeField] private GameObject ControlsMenuPanel;
    [Header("Audio")]
    [SerializeField] private AudioClip menuMusic;

    private AudioManager audioManager => ResourceLocator.instance.AudioManager;
    private Clock clock => ResourceLocator.instance.Clock;

    private void Start() {
        if (clock.ActNum > 5)
            ShowCreditsSubmenu();
        else
            ShowMainMenu();

        audioManager.SetVolume(1.0f);
        audioManager.PlayLoop(menuMusic);
    }

    public void PlayGame() {
        ResourceLocator.instance.Clock.PlayFromStart();
    }

    private void HideAllSubmenus() {
        MainMenuPanel.SetActive(false);
        CreditsMenuPanel.SetActive(false);
        ControlsMenuPanel.SetActive(false);
    }

    public void ShowMainMenu() {
        HideAllSubmenus();
        MainMenuPanel.SetActive(true);
    }

    public void ShowCreditsSubmenu() {
        HideAllSubmenus();
        CreditsMenuPanel.SetActive(true);
    }

    public void ShowControlsSubmenu()
    {
        HideAllSubmenus();
        ControlsMenuPanel.SetActive(true);
    }
}
