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

        ////Act 1 Day 1 letter
        //allLetters.Add(new List<string> { "Dear Agung,<br><br>Your father in uniform! I bet you never thought you’d see that. " +
        //    "Truth be told, neither did I, but at moments like this, we all must do our part, even those of us who are more fit to hold a spade than a gun. " +
        //    "<br><br>Take care of the garden until I come back, won’t you? Here are some sweet potato slips to get you started. " +
        //    "Growing them is a piece of cake. Just plant them in good rich earth and water them every day. " +
        //    "I’ve also sent along some ripe sweet potatoes to stock the stand with in the meantime. ",
        //    "Most of all, take care of our tree. When you water it, I hope you’ll remember the day we planted it together. " +
        //    "This cheeky little myna bird has agreed to carry my mail. " +
        //    "If you see her perched on the tree in the evening, you’ll know there’s a letter from me. " +
        //    "<br><br>I’ll be home soon.<br><br>Love, Dad"});

        ////Act 1 Day 3 letter
        //allLetters.Add(new List<string> { "Dear Agung,<br><br>We’re shipping out. " +
        //    "I’d held onto the rather selfish hope that I might get an assignment behind the lines, a cook maybe, or even a ditch digger. " +
        //    "But no, I'm in the infantry.<br><br>Please try not to worry about me. " +
        //    "So far your old man hasn’t faced anything worse than mosquitoes and monsoon heat. " +
        //    "Word among the men is that the war will be over in another month. ",
        //    "How are things at home? " +
        //    "Old Indah from the farm down the road said she'd look in on you as often as she could. " +
        //    "Don't be afraid to accept help, son. In war, good friends are sometimes the only thing you can rely on. " +
        //    "<br><br>Love, Dad"});

        ////Act 1 Day 5 letter
        //allLetters.Add(new List<string> { "Dear Agung,<br><br>We’re winning the war. At least, that’s what the radio says. " +
        //    "From where we are, winning and losing look about the same: Sitting in muddy ditches and taking cover from shells. " +
        //    "But we press on, advancing into enemy territory. " +
        //    "<br><br>Enemy territory. What are we meant to think of when we hear those words? " +
        //    "Their strange houses, their unfamiliar clothes? " +
        //    "But what I saw was two small children throwing rocks into a creek, just the way you did when you were that age. " +
        //    "Children playing in the water, I think, are the same everywhere. " +
        //    "<br><br>Love, Dad"});

        ////Act 1 Day 7 letter
        //allLetters.Add(new List<string> { "Dear Agung,<br><br>Advance. Retreat. Advance. Retreat. Are we still winning? It’s hard to tell. " +
        //    "There’s always another hill to capture, another river to cross. " +
        //    "The mud, the smoke, and the bombs are always the same. " +
        //    "<br><br>The only thing that changes is me. Artillery doesn’t startle me anymore. " +
        //    "When we eat our tinned rations, I think less and less of the crisp, sweet sugar cane that must be ready to harvest right about now. ",
        //    "I don’t hear anyone saying the war will be over in a month anymore. I’m afraid to think what the future might hold if it drags on. " +
        //    "What will become of us, out here on the front? What will become of the garden? What will become of you? " +
        //    "<br><br>Love, Dad"});

        ////Act 2 Day 1 letter
        //allLetters.Add(new List<string> { "Dear Agung,<br><br>Happy birthday! I’m sorry I couldn’t be there. " +
        //    "I know, that’s what I said last year, and the year before. Where has it all gone? " +
        //    "<br><br>I’ve sent you a present. It was so heavy the myna could barely lift it! " +
        //    "It’s a prayer rug I bought from one of the roadside vendors. " +
        //    "How they keep on throughout it all and still find the strength to make beautiful things I can’t imagine. " ,
        //    "Pray for an end to the war, and for us here at the front. " +
        //    "There isn’t a single day when I don’t pray to see you again. " +
        //    "<br><br>Love, Dad"});

        ////Act 2 Day 3 letter
        //allLetters.Add(new List<string> { "Dear Agung,<br><br>I was reminiscing today. I know, I sound like an old man. " +
        //    "But I was watching the farmers out sowing the fields and it brought back the days when I was growing up. " +
        //    "Planting time was always my favorite season. " +
        //    "<br><br>When your aunts and I were little, Indah would pay us in sweets to chase off the birds who tried to eat the seeds. " +
        //    "Now I suspect she was just keeping us out of the way. But we took our job very seriously! ",
        //    "Indah already seemed older than the hills back then. It’s nice to think that some things back home haven’t changed at all. " +
        //    "<br><br>Love, Dad"});

        ////Act 2 Day 5 letter
        //allLetters.Add(new List<string> { "Dear Agung,<br><br>Your dad is a hero! And no one is more surprised than me. " +
        //    "<br><br>Two of us were scouting ahead through the jungle when we stumbled onto an advance enemy squadron. " +
        //    "They’d stopped to make camp and weren’t keeping watch, and they surrendered to us without firing a shot. " +
        //    "<br><br>I earned a medal for courage. " +
        //    "I can’t say I felt very courageous--they were just lounging around with their tin dishes of stew, and one of them was in the middle of shaving. " +
        //    "Maybe courage never feels the way it seems like it should. " ,
        //    "I’ve enclosed my medal for you. " +
        //    "Its shape makes me think of the view of the stars from our garden at night. Maybe it will bring you luck." +
        //    "<br><br>Love, Dad"});

        ////Act 2 Day 7 letter
        //allLetters.Add(new List<string> { "Dear Agung,<br><br>I spent some time talking with the POWs today. They’re just boys, hardly older than you. " +
        //    "They weren’t angry with us. Tired, mostly. For them, this war has felt like their whole lives. " +
        //    "They’d heard that we treat our captives fairly, and when we burst into their camp, it just didn’t seem worth fighting. " +
        //    "<br><br>They showed me photos of their families and their girls back home. " +
        //    "I promised I’d have the myna deliver a letter for them so their families won’t worry about them. " +
        //    "I know how you’d worry if it were me. " ,
        //    "I don’t see why rounding them up and pointing a gun at them was worth a medal. " +
        //    "Are all acts of bravery like that?" +
        //    "<br><br>Love, Dad"});

        ////Act 3 Day 1 letter
        //allLetters.Add(new List<string> { "Dear Agung,<br><br>I’m sorry. I’m sorry for everything. " +
        //    "It was supposed to be just a few weeks, a blink of an eye and then I’d be home, picking you up to put on my shoulders. " +
        //    "But it's been years since you were small enough to ride on shoulders. I've missed your entire childhood. " +
        //    "<br><br>You're a man now. What kind of man have you become? " +
        //    "There were so many things I was going to teach you, so many things we were going to do together. " +
        //    "These letters are a poor consolation for growing up without a father. " ,
        //    "You’ll tell me I had no choice. But there's always a choice, isn't there? " +
        //    "I could have taken you and fled into the jungle. Anything to escape this endless war. " +
        //    "<br><br>Love, Dad"});

        ////Act 3 Day 3 letter
        //allLetters.Add(new List<string> { "Dear Agung,<br><br>Do you remember how you used to be fascinated by dreams? Nearly every morning you’d tell me yours. " +
        //    "I hope they still intrigue you, because I had a dream last night, and you were in it. " +
        //    "<br><br>We were in the garden, and you were a child again. Our tree had grown so big that it took up the entire sky, but instead of leaves," +
        //    " it had pieces of glass in all different colors, and the wind made them tinkle against each other and sparkle like stars. " +
        //    "Thousands of birds of all kinds came and roosted in the branches. ",
        //    "How big our tree must be now, the real one! It must be almost ready to bear acorns. " +
        //    "Thank you for taking such good care of it. " +
        //    "I like to think of how, when this war is long over, future generations will still be sitting in its shade. " +
        //    "<br><br>Love, Dad"});

        ShowLetter();
    }

    public void ShowLetter() {
        // Doesn't need to take an argument--
        // it always simply reads the first letter on the list and then deletes it
        letterPanel.SetActive(true);

        currentLetter = allLetters[0];
        allLetters.RemoveAt(0);

        //add any items to inventory
        foreach (ItemStack gift in currentLetter.gifts)
        {
            
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
