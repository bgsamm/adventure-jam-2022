using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public Act CurrentAct => Acts[ActNum - 1];
    public Day CurrentDay => CurrentAct.days[DayNum - 1];
    // indexed from 1, not 0
    public int ActNum;
    // indexed from 1, not 0
    public int DayNum;

    [SerializeField] private Cutscene SunriseCutscene;
    [SerializeField] private List<Act> Acts;

    private SceneLoader sceneLoader => ResourceLocator.instance.SceneLoader;
    private AudioManager audioManager => ResourceLocator.instance.AudioManager;
    private CutsceneManager cutsceneManager => ResourceLocator.instance.CutsceneManager;
    private LetterManager letterManager => ResourceLocator.instance.LetterManager;
    private GardenManager gardenManager => ResourceLocator.instance.GardenManager;

    public void PlayFromStart() {
        ActNum = 0;
        StartNextAct();
    }

    public void StartNextDay() {
        void BeginDay() {
            gardenManager.BeginDay();
            cutsceneManager.PlayCutscene(SunriseCutscene,
                delegate {
                    Debug.Log("Next day");
                    audioManager.PlayLoop(CurrentAct.music);
                    // Some acts open with a letter
                    if (DayNum == 1 && CurrentAct.openingLetter != null)
                        letterManager.ShowLetter(CurrentAct.openingLetter, sceneLoader.LoadGardenScene);
                    else
                    {
                        sceneLoader.LoadGardenScene();
                        Debug.Log("Loading the garden scene");
                    }
                });
        }
        DayNum++;
        // If end of current act, proceed to next act
        if (DayNum > CurrentAct.days.Count) {
            StartNextAct();
        }
        else {
            BeginDay();
        }
    }

    public void StartNextAct() {
        ActNum++;
        if (ActNum > Acts.Count) {
            sceneLoader.LoadMenuScene();
        }
        else {
            DayNum = 0;
            // Play opening cutscene, if there is one
            if (CurrentAct.openingCutscene != null)
                cutsceneManager.PlayCutscene(CurrentAct.openingCutscene, StartNextDay);
            else
                StartNextDay();
        }
    }
}
