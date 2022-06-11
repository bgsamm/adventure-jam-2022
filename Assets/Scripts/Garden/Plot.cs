using UnityEngine;

public class Plot : Interactable
{
    public bool ReadyToHarvest => currPlant != null && currPlant.Mature;
    public bool Occupied => currPlant != null;

    [SerializeField] private GameObject interactableFrame;
    private GardenManager garden;
    private InventorySystem inventory;

    private Sprite emptyPlotSprite;
    private Seed currPlant;

    // TESTING ONLY - REMOVE AFTER TESTING, remove once visual elements exist
    [SerializeField] private bool isOccupied;
    [SerializeField] private bool isWatered;
    [SerializeField] private bool isReadyToHarvest;

    private void Awake() {
        interactableFrame.SetActive(false);
        garden = GameObject.FindGameObjectWithTag("GameController").GetComponent<GardenManager>();
    }

    public void Plant(Seed seed) {
        emptyPlotSprite = GetComponent<SpriteRenderer>().sprite;
        currPlant = seed;
        GetComponent<SpriteRenderer>().sprite = currPlant.InventorySprite;
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<SpriteRenderer>().sortingOrder = 0;
        isOccupied = true; // remove after testing
    }

    public void Water() {
        currPlant.Water();
        isWatered = true; // remove after testing
    }

    public void Harvest() {
        if (ReadyToHarvest)
            Debug.Log("Harvesting!");
        // return plant to player
        isOccupied = false; // remove after testing
        currPlant = null;
    }

    public void Grow() {
        if (currPlant == null) return;
        currPlant.Grow();
        isReadyToHarvest = currPlant.Mature; // remove after testing
    }

    public override void StartCanInteract() {
        interactableFrame.SetActive(true);
    }

    public override void StopCanInteract() {
        interactableFrame.SetActive(false);
    }

    public override void Interact() {
        if (ReadyToHarvest) {
            Harvest();
        }
        else {
            Water();
        }
    }
    public void Interact(Seed seed)
    {
        if (!Occupied)
        {
            Debug.Log("Planting on " + gameObject.name);
            Plant(seed);
        }
    }
}
