using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResourceLocator : MonoBehaviour
{
    public static ResourceLocator instance { get; private set; }

    public SceneLoader SceneLoader { get; private set; }
    public CutsceneManager CutsceneManager { get; private set; }
    public LetterManager LetterManager { get; private set; }
    public Clock Clock { get; private set; }
    public InventorySystem InventorySystem { get; private set; }

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(this);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        // initialize resources
        SceneLoader = GetComponent<SceneLoader>();
        CutsceneManager = GetComponent<CutsceneManager>();
        LetterManager = GetComponent<LetterManager>();
        Clock = GetComponent<Clock>();
        InventorySystem = GetComponent<InventorySystem>();
    }
}
