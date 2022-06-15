using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//this script governs the tree gameobject in the Garden scene--NOT the tree scene itself
public class TreeObject : Interactable
{
    [SerializeField] private GameObject interactableFrame;
    [SerializeField] private TextMeshProUGUI interactableText;

    private SceneLoader sceneLoader => ResourceLocator.instance.SceneLoader;
    private Clock clock => ResourceLocator.instance.Clock;

    public override void Interact()
    {
        //if there is no letter, just loading the scene counts as checking for a letter
        if (clock.CurrentDay.letter != null)
            clock.LetterChecked = true;
        sceneLoader.LoadTreeScene();
    }

    public override void StartCanInteract()
    {
        interactableText.text = "Press E to view the tree";
        interactableFrame.SetActive(true);
        interactableText.gameObject.SetActive(true);
    }

    public override void StopCanInteract()
    {
        interactableFrame.SetActive(false);
        interactableText.gameObject.SetActive(false);
    }
}
