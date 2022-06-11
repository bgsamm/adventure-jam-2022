using UnityEngine;

public class Plot : Interactable
{
    public bool Occupied => currPlant != null;

    private int daysWatered;
    private bool wateredToday;
    private bool readyToHarvest;

    [SerializeField] private GameObject interactableFrame;
    private GardenManager garden;
    private InventorySystem inventory;

    private Seed currPlant;
    private SpriteRenderer spriteRenderer;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start() {
        inventory = InventorySystem.instance;
        garden = GardenManager.instance;
        interactableFrame.SetActive(false);
    }

    public void Plant(Seed seed) {
        currPlant = seed;
        spriteRenderer.sprite = currPlant.gameSprites[0];
        spriteRenderer.enabled = true;
        //spriteRenderer.sortingOrder = 0;
    }

    public void Water() {
        if (!wateredToday) {
            //wateredToday = true;
            ++daysWatered;
            spriteRenderer.sprite = currPlant.gameSprites[daysWatered];
            readyToHarvest = daysWatered >= 2;
            Debug.Log("You watered this plant!");
        }
    }

    public void Grow() {
        if (currPlant == null)
            return;
        if (daysWatered >= currPlant.daysToGrow) {
            readyToHarvest = true;
            Debug.Log("This plant has grown!");
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
    }

    public override void StopCanInteract() {
        interactableFrame.SetActive(false);
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
            else
                Water();
        }
    }
}
