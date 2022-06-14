using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Cart : Interactable
{
    [SerializeField] private GameObject interactableFrame;
    [SerializeField] private GameObject interactableText;

    private void Start() { }

    public override void Interact() {
        if (!ResourceLocator.instance.Clock.shopVisited)
        {
            ResourceLocator.instance.SceneLoader.LoadShopScene();
        }
        else
        {
            Debug.Log("Already visited!");
        }
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
