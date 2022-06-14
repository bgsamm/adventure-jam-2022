using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LetterReader : MonoBehaviour
{
    [SerializeField] private GameObject letterPanel;
    [SerializeField] private TextMeshProUGUI letterTextbox;
    [SerializeField] private GameObject continueButton;
    [SerializeField] private GameObject closeButton;

    private LetterManager letterManager;
    private InventorySystem inventory;

    private Letter currentLetter;
    private int pageIndex;

    private void Start() {
        letterManager = ResourceLocator.instance.LetterManager;
        inventory = ResourceLocator.instance.InventorySystem;

        pageIndex = 0;
        currentLetter = letterManager.NextLetter;
        ShowNextPage();
    }

    public void ShowNextPage() {
        letterTextbox.text = currentLetter.pages[pageIndex++];

        if (currentLetter.pages.Count > pageIndex) {
            continueButton.SetActive(true);
            closeButton.SetActive(false);
        }
        else {
            continueButton.SetActive(false);
            closeButton.SetActive(true);
        }
    }

    public void CloseLetter() {
        foreach (var stack in currentLetter.gifts) {
            inventory.AddItems(stack);
        }
        letterManager.LetterEndCallback.Invoke();
    }
}
