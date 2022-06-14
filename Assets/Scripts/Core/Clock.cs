using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    // Should day count reset to 0 on next act?
    // Could be useful for letter counting / plant growth?
    public int day { get; private set; }
    public bool isMorning { get; private set; }
    public bool shopVisited { get; set; }

    [SerializeField] private List<Day> Days;

    private void Start() {
        day = 0;
        isMorning = true; // Game starts in morning
        shopVisited = false;
    }

    // Should be called after/on shop visit, and sleeping
    public void NextDay() {
        ResourceLocator.instance.CutsceneManager.PlayCutscene("Sunrise", ResourceLocator.instance.SceneLoader.LoadGardenScene);

        if (!isMorning) {
            ++day; // Increments day if night already
        }
        isMorning = !isMorning; // flips to morning/dusk

        shopVisited = false;
        GardenManager.instance.GrowOvernight();
        Debug.Log("Next day");
    }
}
