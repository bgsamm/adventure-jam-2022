using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public bool TasksComplete => ShopVisited && TreeWatered && LetterChecked;

    public Act CurrentAct => Acts[ActNum - 1];
    public Day CurrentDay => CurrentAct.days[DayNum - 1];
    // indexed from 1, not 0
    public int ActNum { get; private set; }
    // indexed from 1, not 0
    public int DayNum { get; private set; }

    [SerializeField] private Cutscene SunriseCutscene;
    [SerializeField] private List<Act> Acts;

    [HideInInspector]
    public bool ShopVisited;
    [HideInInspector]
    public bool TreeWatered;
    // checked for letters (i.e. visited the tree)
    // and read the letter if there is one
    [HideInInspector]
    public bool LetterChecked;

    private SceneLoader sceneLoader => ResourceLocator.instance.SceneLoader;
    private CutsceneManager cutsceneManager => ResourceLocator.instance.CutsceneManager;
    private LetterManager letterManager => ResourceLocator.instance.LetterManager;

    public void PlayFromStart() {
        ActNum = 0;
        StartNextAct();
    }

    public void StartNextDay() {
        void BeginDay() {
            ShopVisited = false;
            TreeWatered = false;
            LetterChecked = false;
            //GardenManager.instance.GrowOvernight();
            cutsceneManager.PlayCutscene(SunriseCutscene,
                delegate {
                    Debug.Log("Next day");
                    sceneLoader.LoadScene(CurrentAct.scene);
                });
        }
        DayNum++;
        // If end of current act, proceed to next act
        if (DayNum > CurrentAct.days.Count) {
            StartNextAct();
        }
        else {
            if (DayNum == 1 && CurrentAct.openingLetter != null)
                letterManager.ShowLetter(CurrentAct.openingLetter, BeginDay);
            else
                BeginDay();
        }
    }

    public void StartNextAct() {
        ActNum++;
        if (ActNum > Acts.Count) {
            // TODO: end game
        }
        else {
            DayNum = 0;
            if (CurrentAct.openingCutscene != null)
                cutsceneManager.PlayCutscene(CurrentAct.openingCutscene, StartNextDay);
            else
                StartNextDay();
        }
    }
}
