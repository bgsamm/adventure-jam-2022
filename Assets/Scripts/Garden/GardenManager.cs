using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GardenManager : MonoBehaviour
{
    public bool TasksComplete => ShopVisited && TreeWatered && LetterChecked;
    [HideInInspector]
    public bool ShopVisited;
    [HideInInspector]
    public bool TreeWatered;
    [HideInInspector]
    public bool LetterChecked; // checked for letters (i.e. visited the tree) and read the letter if there is one

    // Maps plot names to the Plants they contain
    private static Dictionary<string, Plant> plantDict;
    // Track the player's position across scenes
    private static Vector3? playerPosition = null;

    private Clock clock => ResourceLocator.instance.Clock;

    private Plot[] plots;
    private GameObject player;

    private void Awake() {
        player = GameObject.FindGameObjectWithTag("Player");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode) {
        // if scene contains player, move player to previous position
        if (player != null && playerPosition != null)
            player.transform.position = (Vector3)playerPosition;
        // grab any plots in the scene
        plots = FindObjectsOfType<Plot>();
        // if first time encountering plots, initialize plantDict
        if (plantDict == null) {
            plantDict = new Dictionary<string, Plant>();
            foreach (var plot in plots) {
                plantDict[plot.name] = null;
            }
        }
        // otherwise, set the Plant for each plot
        else {
            foreach (var plot in plots) {
                Debug.Assert(plantDict.ContainsKey(plot.name));
                plot.CurrentPlant = plantDict[plot.name];
            }
        }
    }

    private void Update() {
        // track player position
        if (player != null)
            playerPosition = player.transform.position;
        // Keep plantDict up-to-date
        foreach (var plot in plots) {
            plantDict[plot.name] = plot.CurrentPlant;
        }
    }

    /// <summary>
    /// Resets task booleans & causes plants to grow
    /// </summary>
    public void BeginDay() {
        // causes player to spawn at starting position
        playerPosition = null;
        // reset tasks
        ShopVisited = false;
        TreeWatered = false;
        LetterChecked = false;
        // Don't grow plants at the start of an act
        if (clock.DayNum > 1)
            GrowPlants();
    }

    private void GrowPlants() {
        foreach (var plant in plantDict.Values) {
            if (plant != null)
                plant.Grow();
        }
    }
}
