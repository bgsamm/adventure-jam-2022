using UnityEngine;

public class Plot : Interactable
{
    public bool Occupied => currPlant != null;

    private int daysWatered;
    private bool wateredToday;
    private int growthStage;
    readonly int stagesToHarvest = 2;
    private bool readyToHarvest;

    [SerializeField] private GameObject interactableFrame;
    [SerializeField] private GameObject interactableText;
    private InventorySystem inventory;

    private Seed currPlant;
    private SpriteRenderer spriteRenderer;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        interactableFrame.SetActive(false);
        growthStage = 0;
    }
    private void Start() {
        inventory = ResourceLocator.instance.InventorySystem;
    }

    public void Plant(Seed seed) {
        currPlant = seed;
        spriteRenderer.sprite = currPlant.gameSprites[0];
        spriteRenderer.enabled = true;
        spriteRenderer.sortingOrder = 0;
    }

    public void Water() {
        if (!wateredToday) {
            wateredToday = true;
            ++daysWatered;
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
        }
    }

    public void Harvest() {
        Debug.Log("Harvesting!");
        inventory.AddItem(currPlant.yield);
        daysWatered = 0;
        readyToHarvest = false;
        currPlant = null;
        spriteRenderer.enabled = false;
    }

    public override void StartCanInteract() {
        interactableFrame.SetActive(true);
        interactableText.SetActive(true);
    }

    public override void StopCanInteract() {
        interactableFrame.SetActive(false);
        interactableText.SetActive(false);
    }

    public override void Interact() {
        if (!Occupied && inventory.selectedStack != null) {
            // should only ever have seeds in your toolbar,
            // but good to check should that ever change
            var selectedSeed = inventory.selectedStack.item as Seed;
            if (selectedSeed != null)
                Plant(selectedSeed);
        }
        else if (Occupied) {
            if (readyToHarvest)
                Harvest();
            else if (!readyToHarvest)
                Water();
        }
    }
}
