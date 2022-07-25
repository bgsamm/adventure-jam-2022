using UnityEngine;

public class Plot : Interactable
{
    public Plant CurrentPlant;
    public bool Occupied => CurrentPlant != null;

    [Header("Ground Sprites")]
    [SerializeField] private Sprite dryGroundSprite;
    [SerializeField] private Sprite wetGroundSprite;
    [Header("Components")]
    [SerializeField] private SpriteRenderer cropSprite;
    [SerializeField] private GameObject waterIcon;
    [SerializeField] private WateringCan wateringCan;
    private SpriteRenderer spriteRenderer;

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
            cropSprite.enabled = true;
            cropSprite.sprite = CurrentPlant.Sprite;
            waterIcon.SetActive(!CurrentPlant.Watered && !CurrentPlant.ReadyToHarvest);
            spriteRenderer.sprite = CurrentPlant.Watered ? wetGroundSprite : dryGroundSprite; 
        }
        else {
            cropSprite.enabled = false;
            waterIcon.SetActive(false);
            spriteRenderer.sprite = dryGroundSprite;
        }
    }

    private void Plant() {
        if (inventory.HoldingSeed) {
            var selectedSeed = (Seed)inventory.selectedStack.item;
            inventory.RemoveItems(selectedSeed, 1);
            CurrentPlant = new Plant(selectedSeed);
        }
    }

    private void Water() {
        wateringCan.gameObject.SetActive(true);
        wateringCan.PlayWateringAnim(delegate {
            CurrentPlant.Water();
            wateringCan.gameObject.SetActive(false);
        });
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
            Water();
        }
    }
}
