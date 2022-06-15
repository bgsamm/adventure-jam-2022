using UnityEngine;
using TMPro;

public class Plot : Interactable
{
    public bool Occupied => currPlant != null;

    private int daysWatered;
    private bool wateredToday;
    private int growthStage;
    readonly int stagesToHarvest = 2;
    private bool readyToHarvest;

    [SerializeField] private GameObject interactableFrame;
    [SerializeField] private TextMeshProUGUI interactableText;
    [SerializeField] private GameObject waterIcon;

    private InventorySystem inventory => ResourceLocator.instance.InventorySystem;

    private Seed currPlant;
    private SpriteRenderer spriteRenderer;

    private string interactMessage;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        interactableFrame.SetActive(false);
        growthStage = 0;
    }

    public void Plant() {
        // should only ever have seeds in your toolbar,
        // but good to check should that ever change
        var stack = inventory.selectedStack;
        if (stack != null) {
            var selectedSeed = stack.item as Seed;
            if (selectedSeed != null) {
                inventory.RemoveItems(selectedSeed, 1);
                currPlant = selectedSeed;
                spriteRenderer.sprite = currPlant.gameSprites[0];
                spriteRenderer.enabled = true;
            }
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
        inventory.AddItems(currPlant.yield);
        daysWatered = 0;
        waterIcon.SetActive(false);
        readyToHarvest = false;
        currPlant = null;
        spriteRenderer.enabled = false;
    }

    public override void StartCanInteract() {
        interactableFrame.SetActive(true);
        if (!Occupied && inventory.selectedStack != null) {
            var selectedSeed = inventory.selectedStack.item as Seed;
            if (selectedSeed != null)
                interactMessage = "Press E to plant";
            else
                interactMessage = "You must select a seed to plant.";
        }
        else if (Occupied) {
            if (readyToHarvest)
                interactMessage = "Press E to harvest";
            else if (!readyToHarvest)
                interactMessage = "Press E to water";
        }

        interactableText.gameObject.SetActive(true);
        interactableText.text = interactMessage;
    }

    public override void StopCanInteract() {
        interactableFrame.SetActive(false);
        interactableText.gameObject.SetActive(false);
    }

    public override void Interact() {
        if (!Occupied) {
            Plant();
        }
        else {
            if (readyToHarvest)
                Harvest();
            else if (!readyToHarvest)
                Water();
        }
    }
}
