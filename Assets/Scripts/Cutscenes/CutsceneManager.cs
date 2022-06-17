using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    public Cutscene NextCutscene { get; private set; }
    public Action CutsceneEndCallback { get; private set; }

    private SceneLoader sceneLoader => ResourceLocator.instance.SceneLoader;

    public void PlayCutscene(Cutscene cutscene, Action callback) {
        NextCutscene = cutscene;
        CutsceneEndCallback = callback;
        sceneLoader.LoadCutsceneScene();
    }
}
