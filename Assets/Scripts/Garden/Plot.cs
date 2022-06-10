using UnityEngine;

public class Plot : MonoBehaviour
{
    // Unserialized when done testing
    public bool readyToHarvest { get { return currPlant != null ? currPlant.Mature : false; } }
    public bool occupied { get { return currPlant != null; } }
    private Plant currPlant;

    // TESTING ONLY - REMOVE AFTER TESTING, remove once visual elements exist
    [SerializeField] private bool isOccupied;
    [SerializeField] private bool isWatered;
    [SerializeField] private bool isReadyToHarvest;

    public Plot() {
        currPlant = null;
        isOccupied = false; // remove after testing
        isWatered = false; // remove after testing
        isReadyToHarvest = false; // remove after testing
    }

    public void Plant(Plant plant) {
        currPlant = plant;
        isOccupied = true; // remove after testing
    }

    public void Water() {
        currPlant.Water();
        isWatered = true; // remove after testing
    }

    public void Harvest() {
        if (readyToHarvest) Debug.Log("Harvesting!");
        // return plant to player
        isOccupied = false; // remove after testing
        currPlant = null;
    }

    public void Grow() {
        if (currPlant == null) return;
        currPlant.Grow();
        isReadyToHarvest = currPlant.Mature; // remove after testing
    }
}
