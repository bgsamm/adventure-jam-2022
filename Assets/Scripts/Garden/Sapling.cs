using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Sapling : Interactable
{
    [SerializeField] private GameObject interactableFrame;
    [SerializeField] private TextMeshProUGUI interactableText;
    [SerializeField] private GameObject waterIcon;

    [SerializeField] private GameEnder gameEnder;

    private bool watered;

    private SpriteRenderer spriteRenderer;

    private string interactMessage;

    private void Awake()
    {
        watered = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
        interactableFrame.SetActive(false);
        waterIcon.SetActive(true);
    }

    public override void StartCanInteract()
    {
        interactableFrame.SetActive(true);
                interactMessage = "Press E to water";
        interactableText.gameObject.SetActive(true);
        interactableText.text = interactMessage;
    }

    public override void StopCanInteract()
    {
        interactableFrame.SetActive(false);
        interactableText.gameObject.SetActive(false);
    }

    public override void Interact()
    {
        if (!watered)
            gameEnder.waterCount--;

        waterIcon.SetActive(false);
        watered = true;
        Debug.Log("You watered this plant!");
    }
}
