using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class LetterManager : MonoBehaviour
{
    [SerializeField]
    private GameObject letterPanel;
    [SerializeField]
    private TextMeshProUGUI letterTextbox;
    [SerializeField]
    private GameObject continueButton;
    [SerializeField]
    private GameObject closeButton;

    private int pageIndex;

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

    public void ShowLetter(Letter currentLetter) {
        // Doesn't need to take an argument--
        // it always simply reads the first letter on the list and then deletes it
        letterPanel.SetActive(true);

        //add any items to inventory
        foreach (ItemStack gift in currentLetter.gifts) {
            //ResourceLocator.instance.InventorySystem.AddItem(ItemStack);
        }
        pageIndex = 0;
        ShowPage(currentLetter, pageIndex);
    }

    public void ShowPage(Letter currentLetter, int pageIndex) {
        letterTextbox.text = currentLetter.text[pageIndex];
        pageIndex++;

        if (currentLetter.text.Count > pageIndex) {
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
