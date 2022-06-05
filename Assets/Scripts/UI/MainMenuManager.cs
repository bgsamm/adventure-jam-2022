using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject OptionsMenu;
    public GameObject CreditsMenu;

    // Start is called before the first frame update
    private void Start() {
        ShowMainMenu();
    }

    // Update is called once per frame
    private void Update() {
        
    }

    public void PlayGame() {
        SceneManager.LoadScene("GardenScene");
    }

    private void HideAllSubmenus() {
        MainMenu.SetActive(false);
        OptionsMenu.SetActive(false);
        CreditsMenu.SetActive(false);
    }

    public void ShowMainMenu() {
        HideAllSubmenus();
        MainMenu.SetActive(true);
    }

    public void ShowOptionsSubmenu() {
        HideAllSubmenus();
        OptionsMenu.SetActive(true);
    }

    public void ShowCreditsSubmenu() {
        HideAllSubmenus();
        CreditsMenu.SetActive(true);
    }
}
