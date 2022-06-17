using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script governs the tree gameobject in the Garden scene - NOT the tree scene itself
public class TreeObject : Interactable
{
    private SceneLoader sceneLoader => ResourceLocator.instance.SceneLoader;

    private void Update() {
        InteractMessage = "Press E to view the tree";
    }

    public override void Interact() {
        sceneLoader.LoadTreeScene();
    }

    public override void StartCanInteract() {
        interactableFrame.SetActive(true);
    }

    public override void StopCanInteract() {
        interactableFrame.SetActive(false);
    }
}
