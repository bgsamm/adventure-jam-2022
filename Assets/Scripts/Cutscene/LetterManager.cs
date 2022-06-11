using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LetterManager : MonoBehaviour
{
    [SerializeField]
    public List<List<string>> allLetters = new List<List<string>>();

    private List<string> currentLetter = new List<string>();

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
    void Awake()
    {
        // If there is an instance, and it's not me, delete myself.
        if (instance != null && instance != this)
            Destroy(this);
        else
            instance = this;

        allLetters.Add(new List<string> { "Dear son,<br><br>Your father in uniform! I bet you never thought you’d see that. " +
            "Truth be told, neither did I, but at moments like this, we all must do our part, even those of us who are more fit to hold a spade than a gun. " +
            "<br><br>Take care of the garden until I come back, won’t you? Here are some sweet potato slips to get you started. " +
            "Growing them is a piece of cake. Just plant them in good rich earth and water them every day. " +
            "I’ve also sent along some ripe sweet potatoes to stock the shop with in the meantime.",
            "Most of all, take care of our tree. When you water it, I hope you’ll remember the day we planted it together. " +
            "This cheeky little mynah has agreed to carry my mail. If you see her perched on the tree in the evening, you’ll know there’s a letter from me."+
            "<br><br>I’ll be home soon.<br>Love, Dad"
        });

        //TEST
        ShowLetter();
    }

    public void ShowLetter()
    {
        //Doesn't need to take an argument--
        //it always simply reads the first letter on the list and then deletes it
        letterPanel.SetActive(true);

        currentLetter = allLetters[0];
        allLetters.RemoveAt(0);

        ShowPage();
    }

    public void ShowPage()
    {
        letterTextbox.text = currentLetter[0];
        currentLetter.RemoveAt(0);

        if (currentLetter.Count > 0)
        {
            continueButton.SetActive(true);
            closeButton.SetActive(false);
        }
        else
        {
            continueButton.SetActive(false);
            closeButton.SetActive(true);
        }
    }

    public void CloseLetter()
    {
        letterPanel.SetActive(false);
    }

}
