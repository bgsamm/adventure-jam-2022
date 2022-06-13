using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class LetterManager : MonoBehaviour
{
    [SerializeField]
    public List<Letter> allLetters;

    private Letter currentLetter;

    [SerializeField]
    private GameObject letterPanel;
    [SerializeField]
    private TextMeshProUGUI letterTextbox;
    [SerializeField]
    private GameObject continueButton;
    [SerializeField]
    private GameObject closeButton;

    public static LetterManager instance { get; private set; }

    // Awake is called before the first frame update
    void Awake() {
        // If there is an instance, and it's not me, delete myself.
        if (instance != null && instance != this)
            Destroy(this);
        else
            instance = this;

        //ShowLetter();
    }

    public void ShowLetter() {
        // Doesn't need to take an argument--
        // it always simply reads the first letter on the list and then deletes it
        letterPanel.SetActive(true);

        currentLetter = allLetters[0];
        allLetters.RemoveAt(0);

        //add any items to inventory
        foreach (ItemStack gift in currentLetter.gifts) {

        }

        ShowPage();
    }

    public void ShowPage() {
        Debug.Log("Showing page");
        letterTextbox.text = currentLetter.text[0];
        currentLetter.text.RemoveAt(0);

        if (currentLetter.text.Count > 0) {
            continueButton.SetActive(true);
            closeButton.SetActive(false);
        }
        else {
            continueButton.SetActive(false);
            closeButton.SetActive(true);
        }
    }

    public void CloseLetter() {
        letterPanel.SetActive(false);
    }
}
