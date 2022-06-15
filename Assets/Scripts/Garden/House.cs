using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class House : Interactable
{
    [SerializeField] private GameObject interactableFrame;
    [SerializeField] private TextMeshProUGUI interactableText;

    private Clock clock => ResourceLocator.instance.Clock;

    private void Start() { }

    public override void Interact() {
        ResourceLocator.instance.Clock.NextDay();
    }

    public override void StartCanInteract() {

        interactableFrame.SetActive(true);
        //if you've completed all three tasks, you can go to sleep
        if (clock.ShopVisited && clock.TreeWatered && clock.LetterChecked)
            interactableText.text = "Press E to sleep";
        else
            interactableText.text = "You aren't tired yet.";
        interactableText.gameObject.SetActive(true);
    }

    public override void StopCanInteract() {
        interactableFrame.SetActive(false);
        interactableText.gameObject.SetActive(false);
    }
}
