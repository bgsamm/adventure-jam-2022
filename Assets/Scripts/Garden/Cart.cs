using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class Cart : Interactable
{
    [SerializeField] private GameObject interactableFrame;
    [SerializeField] private TextMeshProUGUI interactableText;

    private SceneLoader sceneLoader => ResourceLocator.instance.SceneLoader;
    private Clock clock => ResourceLocator.instance.Clock;

    public override void Interact() {
        if (!clock.ShopVisited)
        {
            clock.ShopVisited = true;
            sceneLoader.LoadBarterScene();
        }
        else
            Debug.Log("Already visited!");
    }

    public override void StartCanInteract() {
        if (!clock.ShopVisited)
            interactableText.text = "Press E to enter shop";
        else
            interactableText.text = "Your shop has no customers right now.";
        interactableFrame.SetActive(true);
        interactableText.gameObject.SetActive(true);
    }

    public override void StopCanInteract() {
            interactableFrame.SetActive(false);
            interactableText.gameObject.SetActive(false);
    }
}
