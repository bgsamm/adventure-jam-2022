using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Cart : Interactable
{
    private SceneLoader sceneLoader => ResourceLocator.instance.SceneLoader;
    private GardenManager gardenManager => ResourceLocator.instance.GardenManager;

    private void Update() {
        InteractMessage = !gardenManager.ShopVisited ? "Press E to enter shop" : "Your shop has no customers right now.";
    }

    public override void Interact() {
        if (!gardenManager.ShopVisited) {
            gardenManager.ShopVisited = true;
            sceneLoader.LoadBarterScene();
        }
        else
            Debug.Log("Already visited!");
    }

    public override void StartCanInteract() {
        interactableFrame.SetActive(true);
    }

    public override void StopCanInteract() {
        interactableFrame.SetActive(false);
    }
}
