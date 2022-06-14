using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class House : Interactable
{
    [SerializeField] private GameObject interactableFrame;
    [SerializeField] private GameObject interactableText;

    private void Start() { }

    public override void Interact() {
        ResourceLocator.instance.Clock.NextDay();
    }

    public override void StartCanInteract() {
        interactableFrame.SetActive(true);
        interactableText.SetActive(true);
    }

    public override void StopCanInteract() {
        interactableFrame.SetActive(false);
        interactableText.SetActive(false);
    }
}
