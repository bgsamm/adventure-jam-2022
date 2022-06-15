using UnityEngine;

public class GardenManager : MonoBehaviour
{
    private GameObject[] plots; // TODO grab plots from children
    private const string PLOT_TAG = "Plot";

    private InventorySystem inventory => ResourceLocator.instance.InventorySystem;

    private void Awake() {
        plots = GameObject.FindGameObjectsWithTag(PLOT_TAG);
        foreach (GameObject p in plots) {
            p.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    public void GrowOvernight() {
        foreach (GameObject p in plots) {
            Plot currPlot = p.GetComponent<Plot>();
            if (currPlot.Occupied) {
                currPlot.Grow();
            }
        }
    }
}
