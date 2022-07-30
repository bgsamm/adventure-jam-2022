using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GardenManager : MonoBehaviour
{
    public bool CantEat => !FoodEaten && !inventory.HasFood;
    public bool TasksComplete => (ShopVisited && TreeWatered && LetterRead && (FoodEaten || CantEat)) || (clock.ActNum == 4 && TreeWatered);
    [HideInInspector] public bool ShopVisited;
    [HideInInspector] public bool TreeVisited;
    [HideInInspector] public bool TreeWatered;
    [HideInInspector] public bool LetterRead;
    [HideInInspector] public bool FoodEaten;

    // Maps plot names to the Plants they contain
    private static Dictionary<string, Plant> plantDict;
    // Track the player's position across scenes
    private static Vector3? playerPosition = null;

    private UnityAudioManager audioManager => ResourceLocator.instance.AudioManager;
    private Clock clock => ResourceLocator.instance.Clock;
    private Inventory inventory => ResourceLocator.instance.InventorySystem;

    private Plot[] plots;
    private GameObject player;

    public bool[] acornFallen = new bool[9];

    private void Awake() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode) {
        // if scene doesn't contain player, this isn't a garden scene
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return;

        // move player to previous position
        if (playerPosition != null)
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

        // if bird is present and tree has not been checked, play birdsong
        if (clock.CurrentDay.birdPresent && !TreeVisited) {
            audioManager.PlayOneShot(audioManager.birdsong);
        }
    }

    private void Update() {
        if (player == null) return;
        // track player position
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
        TreeVisited = false;
        TreeWatered = false;
        LetterRead = false;
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
