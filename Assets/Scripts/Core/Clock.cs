using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    // Should day count reset to 0 on next act?
    // Could be useful for letter counting / plant growth?
    public Day CurrentDay => Days[day];
    public bool IsMorning { get; private set; }
    public bool ShopVisited { get; set; }

    [SerializeField] private List<Day> Days;
    private int day;

    private void Start() {
        day = 0;
        IsMorning = true; // Game starts in morning
        ShopVisited = false;
    }

    // Should be called after/on shop visit, and sleeping
    public void NextDay() {
        ResourceLocator.instance.CutsceneManager.PlayCutscene("Sunrise", ResourceLocator.instance.SceneLoader.LoadGardenScene);

        if (!IsMorning) {
            ++day; // Increments day if night already
        }
        IsMorning = !IsMorning; // flips to morning/dusk

        ShopVisited = false;
        GardenManager.instance.GrowOvernight();
        Debug.Log("Next day");
    }
}
