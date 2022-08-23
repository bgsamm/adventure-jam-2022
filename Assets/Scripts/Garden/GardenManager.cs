using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GardenManager : MonoBehaviour
{
    public AudioClip birdSound;

    public bool CantEat => !FoodEaten && !inventory.HasFood;
    public bool TasksComplete => (ShopVisited && TreeWatered && LetterRead && (FoodEaten || CantEat)) || (clock.ActNum == 4 && TreeWatered);
    [HideInInspector] public bool ShopVisited;
    [HideInInspector] public bool TreeVisited;
    [HideInInspector] public bool TreeWatered;
    [HideInInspector] public bool LetterRead;
    [HideInInspector] public bool FoodEaten;

    [HideInInspector] public string CurrentTask; 

    // Maps plot names to the Plants they contain
    private static Dictionary<string, Plant> plantDict;
    // Track the player's position across scenes
    private static Vector3? playerPosition = null;

    private AudioManager audioManager => ResourceLocator.instance.AudioManager;
    private Clock clock => ResourceLocator.instance.Clock;
    private Inventory inventory => ResourceLocator.instance.InventorySystem;

    private Plot[] plots;
    private GameObject player;

    [HideInInspector] public bool[] acornFallen = new bool[9];

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
        // if first day of the act, initialize plantDict
        //re-initializes every act because the plots get smaller
        if (plantDict == null) {
            Debug.Log("Day 1: generating plots");
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

        // if bird is present and tree has not been checked, play birdsong
        if (clock.CurrentDay.birdPresent && !TreeVisited) {
            audioManager.PlayOneShot(birdSound);
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

        if (!ShopVisited)
        {
            CurrentTask = "Visit shop";
        }
        else if (!TreeWatered)
        {
            CurrentTask = "Water tree";
        }
        else if (!LetterRead)
        {
            CurrentTask = "Read letter";
        }
        else if (!FoodEaten && !CantEat)
        {
            CurrentTask = "Have something to eat";
        }
        else
        {
            CurrentTask = "";
        }
    }

    /// <summary>
    /// Resets task booleans & causes plants to grow
    /// </summary>
    public void BeginDay() {
        Debug.Log("Beginning day in Garden Manager");
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
        else if (clock.ActNum == 2 || clock.ActNum == 3) {
            Debug.Log("Act end: Harvesting plants");
            //start of acts 2 and 3
            //harvests all plants (regardless of growth stage) 
            for (int x = 0; x < plantDict.Count; x++) {
                if (plantDict.ElementAt(x).Value != null)
                    plantDict.ElementAt(x).Value.Harvest();
            }
            //deletes the plant dictionary (automatically triggers rebuilding it at next act opening)
            plantDict.Clear();
            for (int x = 0; x < inventory.stacks.Count; x++) {
                //perishable foods get deleted between acts
                if (inventory.stacks[x].item.Perishable) {
                    inventory.RemoveItems(inventory.stacks[x]);
                }

                //large stacks get reduced
                if (inventory.stacks[x].count > 5) {
                    inventory.stacks[x].count = (int)(inventory.stacks[x].count * 0.5f + 2);
                }
            }
        }

    }

    private void GrowPlants() {
        foreach (var plant in plantDict.Values) {
            if (plant != null) {
                plant.Grow();
            }
        }
    }
}

