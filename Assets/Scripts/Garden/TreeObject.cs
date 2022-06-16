using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script governs the tree gameobject in the Garden scene - NOT the tree scene itself
public class TreeObject : Interactable
{
    private SceneLoader sceneLoader => ResourceLocator.instance.SceneLoader;
    private Clock clock => ResourceLocator.instance.Clock;

    private void Update() {
        InteractMessage = "Press E to view the tree";
    }

    public override void Interact() {
        // if there is no letter, just loading the scene counts as checking for a letter
        if (clock.CurrentDay.letter != null)
            clock.LetterChecked = true;
        sceneLoader.LoadTreeScene();
    }

    public override void StartCanInteract() {
        interactableFrame.SetActive(true);
    }

    public override void StopCanInteract() {
        interactableFrame.SetActive(false);
    }
}
