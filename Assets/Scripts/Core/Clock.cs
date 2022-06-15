using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public Act CurrentAct => Acts[act];
    public Day CurrentDay => CurrentAct.days[day];

    public bool IsMorning { get; private set; }
    public bool ShopVisited;
    public bool TreeWatered;
    // checked for letters (i.e. visited the tree)
    // and read the letter if there is one
    public bool LetterChecked;

    [SerializeField] private List<Act> Acts;
    private int act, day;

    private CutsceneManager cutsceneManager => ResourceLocator.instance.CutsceneManager;
    private SceneLoader sceneLoader => ResourceLocator.instance.SceneLoader;

    private void Start() {
        day = 0;
        act = 0;
        IsMorning = true; // Game starts in morning
        ShopVisited = false;
        TreeWatered = false;
        LetterChecked = false;
    }

    // Should be called after/on shop visit, and sleeping
    public void NextDay() {
        cutsceneManager.PlayCutscene("Sunrise", sceneLoader.LoadGardenScene);

        // Increments day if night already
        if (!IsMorning)
            day++;
        IsMorning = !IsMorning; // flips to morning/dusk

        ShopVisited = false;
        TreeWatered = false;
        LetterChecked = false;
        //GardenManager.instance.GrowOvernight();
        Debug.Log("Next day");
    }

    public void NextAct() {
        day = 0;
        act++;
    }
}
