using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sapling : Interactable
{
    [SerializeField] private GameObject waterIcon;
    [SerializeField] private GameEnder gameEnder;

    private bool watered;

    private void Awake() {
        watered = false;
        interactableFrame.SetActive(false);
        waterIcon.SetActive(true);
    }

    private void Update() {
        InteractMessage = !watered ? "Press E to water" : "";
    }

    public override void StartCanInteract() {
        interactableFrame.SetActive(true);
    }

    public override void StopCanInteract() {
        interactableFrame.SetActive(false);
    }

    public override void Interact() {
        if (!watered)
            gameEnder.waterCount--;

        waterIcon.SetActive(false);
        watered = true;
        Debug.Log("You watered this plant!");
    }
}
