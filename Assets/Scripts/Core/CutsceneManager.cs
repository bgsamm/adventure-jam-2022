using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField] private List<Cutscene> cutscenes;

    public Cutscene NextCutscene { get; private set; }
    public Action CutsceneEndCallback { get; private set; }

    private SceneLoader sceneLoader => ResourceLocator.instance.SceneLoader;
    private Dictionary<string, Cutscene> cutsceneDict;

    private void Awake() {
        // build the cutscene dictionary
        cutsceneDict = new Dictionary<string, Cutscene>();
        foreach (var cutscene in cutscenes) {
            cutsceneDict.Add(cutscene.name, cutscene);
        }
    }

    public void PlayCutscene(string name, Action callback) {
        if (!cutsceneDict.ContainsKey(name)) {
            Debug.LogError($"No cutscene exists with the name '{name}.'");
            return;
        }
        NextCutscene = cutsceneDict[name];
        CutsceneEndCallback = callback;
        sceneLoader.LoadCutsceneScene();
    }
}
