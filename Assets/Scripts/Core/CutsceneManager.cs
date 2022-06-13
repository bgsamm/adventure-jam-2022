using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public delegate void CallbackEvent();

public class CutsceneManager : MonoBehaviour
{
    [SerializeField] private List<Cutscene> cutscenes;

    public Cutscene NextCutscene { get; private set; }
    public CallbackEvent CutsceneEndCallback { get; private set; }

    private SceneLoader sceneLoader;
    private Dictionary<string, Cutscene> cutsceneDict;

    private void Awake() {
        // build the cutscene dictionary
        cutsceneDict = new Dictionary<string, Cutscene>();
        foreach (var cutscene in cutscenes) {
            cutsceneDict.Add(cutscene.name, cutscene);
        }
    }

    private void Start() {
        sceneLoader = ResourceLocator.instance.SceneLoader;
    }

    public void PlayCutscene(string name, CallbackEvent callback) {
        if (!cutsceneDict.ContainsKey(name)) {
            Debug.LogError($"No cutscene exists with the name '{name}.'");
            return;
        }
        NextCutscene = cutsceneDict[name];
        CutsceneEndCallback = callback;
        sceneLoader.LoadCutsceneScene();
    }
}
