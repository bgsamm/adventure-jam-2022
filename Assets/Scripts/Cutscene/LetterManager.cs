using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class LetterManager : MonoBehaviour
{
    //storing the letters externally in a JSON file
    [SerializeField]
    private TextAsset lettersJSON;

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
    void Awake() {
        // If there is an instance, and it's not me, delete myself.
        if (instance != null && instance != this)
            Destroy(this);
        else
            instance = this;

        //Act 1 Day 1 letter
        allLetters.Add(new List<string> { "Dear Agung,<br><br>Your father in uniform! I bet you never thought you’d see that. " +
            "Truth be told, neither did I, but at moments like this, we all must do our part, even those of us who are more fit to hold a spade than a gun. " +
            "<br><br>Take care of the garden until I come back, won’t you? Here are some sweet potato slips to get you started. " +
            "Growing them is a piece of cake. Just plant them in good rich earth and water them every day. " +
            "I’ve also sent along some ripe sweet potatoes to stock the stand with in the meantime. ",
            "Most of all, take care of our tree. When you water it, I hope you’ll remember the day we planted it together. " +
            "This cheeky little myna bird has agreed to carry my mail. " +
            "If you see her perched on the tree in the evening, you’ll know there’s a letter from me. " +
            "<br><br>I’ll be home soon.<br><br>Love, Dad"});

        //Act 1 Day 3 letter
        allLetters.Add(new List<string> { "Dear Agung,<br><br>We’re shipping out. " +
            "I’d held onto the rather selfish hope that I might get an assignment behind the lines, a cook maybe, or even a ditch digger. " +
            "But no, I'm in the infantry.<br><br>Please try not to worry about me. " +
            "So far your old man hasn’t faced anything worse than mosquitoes and monsoon heat. " +
            "Word among the men is that the war will be over in another month. ",
            "How are things at home? " +
            "Old Indah from the farm down the road said she'd look in on you as often as she could. " +
            "Don't be afraid to accept help, son. In war, good friends are sometimes the only thing you can rely on. " +
            "<br><br>Love, Dad"});

        //Act 1 Day 5 letter
        allLetters.Add(new List<string> { "Dear Agung,<br><br>We’re winning the war. At least, that’s what the radio says. " +
            "From where we are, winning and losing look about the same: Sitting in muddy ditches and taking cover from shells. " +
            "But we press on, advancing into enemy territory. ",
            "Enemy territory. What are we meant to think of when you hear those words? " +
            "Their strange houses, their unfamiliar clothes? " +
            "But what I saw was two small children throwing rocks into a creek, just the way you did when you were that age. " +
            "<br><br>Children playing in the water, I think, are the same everywhere. " +
            "<br><br>Love, Dad"});

        //Act 1 Day 7 letter
        allLetters.Add(new List<string> { "Dear Agung,<br><br>Advance. Retreat. Advance. Retreat. Are we still winning? It’s hard to tell. " +
            "There’s always another hill to capture, another river to cross. " +
            "The mud, the smoke, and the bombs are always the same. " +
            "<br><br>The only thing that changes is me. Artillery doesn’t startle me anymore. " +
            "When we eat our tinned rations, I think less and less of the crisp, sweet sugar cane that must be ready to harvest right about now. ",
            "I don’t hear anyone saying the war will be over in a month anymore. I’m afraid to think what the future might hold if it drags on. " +
            "What will become of us, out here on the front? What will become of the garden? What will become of you? " +
            "<br><br>Love, Dad"});

        //Act 2 Day 1 letter
        allLetters.Add(new List<string> { "Dear Agung,<br><br>Advance. Retreat. Advance. Retreat. Are we still winning? It’s hard to tell. " +
            "There’s always another hill to capture, another river to cross. " +
            "The mud, the smoke, and the bombs are always the same. " +
            "<br><br>The only thing that changes is me. Artillery doesn’t startle me anymore. " +
            "When we eat our tinned rations, I think less and less of the crisp, sweet sugar cane that must be ready to harvest right about now. ",
            "I don’t hear anyone saying the war will be over in a month anymore. I’m afraid to think what the future might hold if it drags on. " +
            "What will become of us, out here on the front? What will become of the garden? What will become of you? " +
            "<br><br>Love, Dad"});

        ShowLetter();
    }

    public void ShowLetter() {
        // Doesn't need to take an argument--
        // it always simply reads the first letter on the list and then deletes it
        letterPanel.SetActive(true);

        currentLetter = allLetters[0];
        allLetters.RemoveAt(0);

        ShowPage();
    }

    public void ShowPage() {
        letterTextbox.text = currentLetter[0];
        currentLetter.RemoveAt(0);

        if (currentLetter.Count > 0) {
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
