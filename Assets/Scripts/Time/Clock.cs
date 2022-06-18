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
    private UnityAudioManager audioManager => ResourceLocator.instance.AudioManager;
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
                    sceneLoader.LoadScene(CurrentAct.sceneName);
                });
        }
        DayNum++;
        // If end of current act, proceed to next act
        if (DayNum > CurrentAct.days.Count) {
            StartNextAct();
        }
        else {
            // Some acts open with a letter
            if (DayNum == 1 && CurrentAct.openingLetter != null)
                letterManager.ShowLetter(CurrentAct.openingLetter, BeginDay);
            else
                BeginDay();
        }
    }

    public void StartNextAct() {
        void StartAct() {
            StartNextDay();
            audioManager.PlayLoop(CurrentAct.music);
        }

        ActNum++;
        if (ActNum > Acts.Count) {
            sceneLoader.LoadMenuScene();
        }
        else {
            DayNum = 0;
            if (CurrentAct.openingCutscene != null)
                cutsceneManager.PlayCutscene(CurrentAct.openingCutscene, StartAct);
            else
                StartAct();
        }
    }
}
