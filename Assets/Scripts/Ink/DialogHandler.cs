using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using Ink.Runtime;
using TMPro;

// Used to interface with an Ink story.
// It displays the text of the story one paragraph at a time, generates response buttons.
public class DialogHandler : MonoBehaviour
{
    // The Ink file containing the game's dialog
    [SerializeField] private TextAsset inkJSONAsset;

    private Clock clock => ResourceLocator.instance.Clock;
    private Story story;
    private Action dialogEndCallback;

    // UI elements that will display the NPC name & dialog
    [SerializeField] private TextMeshProUGUI sceneText;
    [SerializeField] private TextMeshProUGUI speakerLabel;
    [SerializeField] private GameObject continueButton;
    [SerializeField] private GameObject dontTradeButton;

    private void Awake() {
        // NOTE: Reloading the story wipes the Ink state machine
        story = new Story(inkJSONAsset.text);
    }

    // Starts a particular dialogue knot
    public void StartDialogue(string knot, Action callback) {
        dialogEndCallback = callback;
        // Bind a function to be called once the dialog thread is over
        // (calls are made in the Ink file)
        story.BindExternalFunction("onDialogEnd", EndDialogue);

        speakerLabel.text = clock.CurrentDay.NPC.name;
        continueButton.SetActive(true);
        dontTradeButton.SetActive(false);

        story.ChoosePathString(knot);
        ContinueStory(); // start the dialogue
    }

    // This is the main function called every time the story changes
    public void ContinueStory() {
        // Read all the content until we can't continue any more
        if (story.canContinue) {
            string text = story.Continue();
            // Skip blank lines
            while (string.IsNullOrWhiteSpace(text) && story.canContinue) {
                text = story.Continue();
            }
            // Display the text on screen! (unless it's blank)
            if (!string.IsNullOrWhiteSpace(text))
                sceneText.text = text;
        }
        // The story CAN'T continue - end thread
        else {
            EndDialogue();
        }
    }

    public void EndDialogue() {
        sceneText.text = "What do you have today?";
        continueButton.SetActive(false);
        dontTradeButton.SetActive(true);
        dialogEndCallback.Invoke();
    }
}
