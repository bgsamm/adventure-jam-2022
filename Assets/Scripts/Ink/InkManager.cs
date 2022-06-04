using UnityEngine;
using UnityEngine.UI;
using System;
using Ink.Runtime;
using TMPro;
using System.Collections.Generic;

public class InkManager : MonoBehaviour {
	//this is a basic script that interfaces with an Ink story
	//it displays the text of the story one paragraph at a time, generates response buttons, and displays a speaker portrait

	public static InkManager instance { get; private set; }

	public static event Action<Story> OnCreateStory;
	
	[SerializeField] //the game story
	private TextAsset inkJSONAsset;
	public Story story;
	//you should ALWAYS reference the Ink story through this script

	//UI elements that will display the text and images
	[SerializeField] private GameObject dialoguePanel;
	[SerializeField] private TextMeshProUGUI sceneText;
	[SerializeField] private GameObject buttonHolder;
	[SerializeField] private Image speakerImage;
	[SerializeField] private TextMeshProUGUI speakerLabel;

	// UI Prefabs
	[SerializeField]
	private Button buttonPrefab;

	//a list of the tags in the current knot
	//used to determine display properties such as the character who's speaking
	[HideInInspector]public List<string> currentTags = new List<string>(); 

	string currentCharacter = ""; //used to check whether the character is changing

	[HideInInspector] public bool dialogueMode = false;
	//a mode for when the response buttons are on the screen--tells the story not to advance on click or spacebar
	[HideInInspector] public bool choiceMode = false; 


	private void Awake()
	{
		// If there is an instance, and it's not me, delete myself.
		if (instance != null && instance != this)
			Destroy(this);
		else
			instance = this;

		story = new Story(inkJSONAsset.text); //load the story
		if (OnCreateStory != null) OnCreateStory(story);
		//Note: If we reload the story, it wipes the Ink state machine
		//So be sure not to destroy this when unloading the scene
	}


    void Update() //tracking keypresses
	{
		if (Input.GetKeyDown(KeyCode.Space) && dialogueMode)
		{
			CheckRefresh();
		}
	}

	public void CheckRefresh() 
		//just a quick check that makes sure the game isn't paused or busy before printing the next screen of text
    {
		if (!choiceMode && dialogueMode)
		{
			RefreshView();
		}
	}

	// starts a particular dialogue knot
	public void StartDialogue(string knot)
	{
		if (!dialogueMode) //start
		{
			dialogueMode = true;

			dialoguePanel.SetActive(true);

			story.ChoosePathString(knot);

			CheckRefresh(); //start the dialogue scene
		}
	}

	// This is the main function called every time the story changes. It does a few things:
	// Destroys all the old content and choices.
	// Continues over all the lines of text, then displays all the choices. If there are no choices, the dialogue is finished
	public void RefreshView()
	{
		//deletes any existing buttons
		foreach (Transform child in buttonHolder.transform)
		{
			GameObject.Destroy(child.gameObject);
		}

		// Read all the content until we can't continue any more
		if (story.canContinue)
		{
			// Continue gets the next line of the story
			string text = story.Continue();
			while (string.IsNullOrWhiteSpace(text)) //if there's a blank line, skips forward until it gets to a non-blank line
			{
				if (story.canContinue)
				{
					text = story.Continue();
				}
				else
				{
					break;
				}
			}

			//retrieves the hashtags from the knot
			currentTags = story.currentTags;

			if (!string.IsNullOrWhiteSpace(text))
			{
				// Display the text on screen! (unless it's blank)
				CreateContentView(text);
			}
		}
		//else //the story CAN'T continue--either we're at a choice or we're at the end of a thread
		// Display all the choices, if there are any
		else
		{
			if (story.currentChoices.Count > 0)
			{
				//choice mode--tells the game not to advance on spacebar
				choiceMode = true;

				for (int i = 0; i < story.currentChoices.Count; i++)
				{
					Choice choice = story.currentChoices[i];
					Button button = CreateChoiceView(choice.text.Trim());
					// Tell the button what to do when we press it
					button.onClick.AddListener(delegate
					{
						OnClickChoiceButton(choice);
					});
				}
			}
			// If we've read all the content and there's no choices, the dialogue is finished
			if (!story.canContinue && story.currentChoices.Count == 0)
			{
				EndDialogue();
			}
		}
		
	}

		// When we click the choice button, tell the story to choose that choice
		void OnClickChoiceButton(Choice choice) 
		{
			choiceMode = false; //unpauses so that click and spacebar will work again
			story.ChooseChoiceIndex(choice.index);
			ClearText(); //clears the text from the previous scene
			RefreshView();
		}

		// Displays the text of the knot, plus the appropriate character
		void CreateContentView(string text)
		{
			DisplaySpeaker();
			sceneText.text += text;
		}

	//just blanks out the text box. Putting this in its own function for easy reference, and in case we want to make it do anything fancier
	void ClearText() 
		{
			sceneText.text = "";
		}

		// Creates a button showing the choice text
		Button CreateChoiceView(string text) {

			// Creates the button from a prefab
			Button choice = Instantiate(buttonPrefab, transform.position, transform.rotation) as Button;
			choice.transform.SetParent(buttonHolder.transform, false);

			// Gets the text from the button prefab
			TextMeshProUGUI choiceText = choice.GetComponentInChildren<TextMeshProUGUI>();
			choiceText.text = text;

			return choice;
		}

		//displays the name and portrait of the person speaking
		public void DisplaySpeaker()
		{
			string lastCharacter; //the name of the previously-displayed character for the same check
			lastCharacter = currentCharacter; //for checking if the character has changed
			Sprite characterSprite = null; //(should never actually display this file, it's just a fallback)

			//displays the character portrait that matches the story tag
			//NOTE: ALL tags are assumed to be character portraits currently, don't use them for anything else
			if (currentTags.Count > 0)
			{
			currentCharacter = currentTags[0];
				characterSprite = Resources.Load<Sprite>($"Characters/{currentCharacter}");
			speakerImage.sprite = characterSprite;
			speakerImage.gameObject.SetActive(true);
			speakerLabel.text = currentCharacter;
		}
			else
			{
			currentCharacter = "";
				ClearSpeaker();
			}

			if (currentCharacter != lastCharacter) //there's been a speaker change: signal to clear the text
			{
				ClearText();
			}
		}

	public void ClearSpeaker()
	{
		speakerImage.gameObject.SetActive(false);
		speakerLabel.text = "";
	}

	public void EndDialogue()
    {
		ClearSpeaker();
		ClearText();
		choiceMode = false;
		dialogueMode = false;
		dialoguePanel.SetActive(false);
    }
}
