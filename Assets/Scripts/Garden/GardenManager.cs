using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GardenManager : MonoBehaviour
{
    public bool TasksComplete => (ShopVisited && TreeWatered && LetterChecked && (FoodEaten || !inventory.HasFood)) || (clock.ActNum == 4 && TreeWatered);
    [HideInInspector] public bool ShopVisited;
    [HideInInspector] public bool TreeWatered;
    [HideInInspector] public bool LetterChecked; // checked for letters (i.e. visited the tree) and read the letter if there is one
    [HideInInspector] public bool FoodEaten;

    // Maps plot names to the Plants they contain
    private static Dictionary<string, Plant> plantDict;
    // Track the player's position across scenes
    private static Vector3? playerPosition = null;

    private Inventory inventory => ResourceLocator.instance.InventorySystem;
    private Clock clock => ResourceLocator.instance.Clock;

    public UnityAudioManager audioManager => ResourceLocator.instance.SFXManager;

    private Plot[] plots;
    private GameObject player;

    public bool[] acornFallen = new bool[9];

    private void Awake() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode) {
        // if scene contains player, move player to previous position
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null && playerPosition != null)
            player.transform.position = (Vector3)playerPosition;
        // grab any plots in the scene
        plots = FindObjectsOfType<Plot>();
        if (plots.Length > 0) {
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
        /*
        // if bird is present and tree is unread, plays birdsong
        if (clock.CurrentDay.birdPresent && !LetterChecked) {
            audioManager.PlayOneShot(audioManager.birdsong);
        }
        */
    }

    private void Update() {
        // track player position
        if (player != null)
            playerPosition = player.transform.position;
        // Keep plantDict up-to-date
        if (plantDict != null) {
            foreach (var plot in plots) {
                plantDict[plot.name] = plot.CurrentPlant;
            }
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
        FoodEaten = false;
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
