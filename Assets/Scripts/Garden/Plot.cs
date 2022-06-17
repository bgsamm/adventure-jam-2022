using UnityEngine;

public class Plot : Interactable
{
    public Plant CurrentPlant;
    public bool Occupied => CurrentPlant != null;

    [SerializeField] private SpriteRenderer waterIcon;
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private SpriteRenderer wateredGroundSprite;

    private InventorySystem inventory => ResourceLocator.instance.InventorySystem;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        interactableFrame.SetActive(false);
    }

    private void Update() {
        InteractMessage = "";
        if (!Occupied && inventory.HoldingSeed) {
            InteractMessage = "Press E to plant";
        }
        else if (Occupied) {
            if (CurrentPlant.ReadyToHarvest)
                InteractMessage = "Press E to harvest";
            else if (!CurrentPlant.Watered)
                InteractMessage = "Press E to water";
        }

        if (CurrentPlant != null) {
            spriteRenderer.enabled = true;
            spriteRenderer.sprite = CurrentPlant.Sprite;
            waterIcon.enabled = !CurrentPlant.Watered && !CurrentPlant.ReadyToHarvest;
            wateredGroundSprite.enabled = CurrentPlant.Watered; 
        }
        else {
            spriteRenderer.enabled = false;
            waterIcon.enabled = false;
            wateredGroundSprite.enabled = false;
        }
    }

    public void Plant() {
        if (inventory.HoldingSeed) {
            var selectedSeed = (Seed)inventory.selectedStack.item;
            inventory.RemoveItems(selectedSeed, 1);
            CurrentPlant = new Plant(selectedSeed);
        }
    }

    public override void StartCanInteract() {
        interactableFrame.SetActive(true);
    }

    public override void StopCanInteract() {
        interactableFrame.SetActive(false);
    }

    public override void Interact() {
        if (!Occupied) {
            Plant();
        }
        else if (CurrentPlant.ReadyToHarvest) {
            CurrentPlant.Harvest();
            CurrentPlant = null;
        }
        else if (!CurrentPlant.Watered) {
            CurrentPlant.Water();
        }
    }
}
