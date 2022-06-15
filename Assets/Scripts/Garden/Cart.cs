using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Cart : Interactable
{
    //[SerializeField] private GameObject interactableFrame;
    //[SerializeField] private GameObject interactableText;

    private SceneLoader sceneLoader => ResourceLocator.instance.SceneLoader;
    private Clock clock => ResourceLocator.instance.Clock;

    public override void Interact() {
        if (!clock.ShopVisited)
            sceneLoader.LoadBarterScene();
        else
            Debug.Log("Already visited!");
    }

    public override void StartCanInteract() {
        //interactableFrame.SetActive(true);
        //interactableText.SetActive(true);
    }

    public override void StopCanInteract() {
        //interactableFrame.SetActive(false);
        //interactableText.SetActive(false);
    }
}
