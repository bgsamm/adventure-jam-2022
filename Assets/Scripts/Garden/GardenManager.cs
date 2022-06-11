using UnityEngine;

public class GardenManager : MonoBehaviour
{
    public static GardenManager instance { get; private set; }
    private GameObject[] plots; // TODO grab plots from children

    public Plant[] Plants;

    const string PLOT_TAG = "Plot";

    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != null && instance != this) {
            Destroy(gameObject);
        }

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
