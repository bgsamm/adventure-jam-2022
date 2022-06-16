using UnityEngine;

public class Plot : Interactable
{
    public bool Occupied => currPlant != null;

    private int daysWatered;
    private bool wateredToday;
    private int growthStage;
    private readonly int stagesToHarvest = 2;
    private bool readyToHarvest;

    [SerializeField] private GameObject waterIcon;

    private InventorySystem inventory => ResourceLocator.instance.InventorySystem;

    private Seed currPlant;
    private SpriteRenderer spriteRenderer;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        interactableFrame.SetActive(false);
        growthStage = 0;
    }

    private void Update() {
        InteractMessage = "";
        if (!Occupied && inventory.HoldingSeed) {
            InteractMessage = "Press E to plant";
        }
        else if (Occupied) {
            if (readyToHarvest)
                InteractMessage = "Press E to harvest";
            else if (!wateredToday)
                InteractMessage = "Press E to water";
        }
    }

    public void Plant() {
        if (inventory.HoldingSeed) {
            var selectedSeed = (Seed)inventory.selectedStack.item;
            inventory.RemoveItems(selectedSeed, 1);
            currPlant = selectedSeed;
            spriteRenderer.sprite = currPlant.gameSprites[0];
            spriteRenderer.enabled = true;
        }
    }

    public void Water() {
        if (!wateredToday) {
            wateredToday = true;
            ++daysWatered;
            waterIcon.SetActive(false);
            Debug.Log("You watered this plant!");
        }
    }

    public void Grow() {
        if (currPlant == null || readyToHarvest)
            return;
        if (daysWatered >= currPlant.daysToGrow) {
            daysWatered = 0;
            ++growthStage;
            spriteRenderer.sprite = currPlant.gameSprites[growthStage];
            readyToHarvest = growthStage >= stagesToHarvest;
            wateredToday = false;
            if (!readyToHarvest)
                waterIcon.SetActive(true);
        }
    }

    public void Harvest() {
        Debug.Log("Harvesting!");
        foreach (ItemStack crop in currPlant.yield)
            inventory.AddItems(crop);
        daysWatered = 0;
        waterIcon.SetActive(false);
        readyToHarvest = false;
        currPlant = null;
        spriteRenderer.enabled = false;
    }

    public override void StartCanInteract() {
        interactableFrame.SetActive(true);
    }

    public override void StopCanInteract() {
        interactableFrame.SetActive(false);
    }

    public override void Interact() {
        if (!Occupied)
            Plant();
        else if (readyToHarvest)
            Harvest();
        else
            Water();
    }
}
