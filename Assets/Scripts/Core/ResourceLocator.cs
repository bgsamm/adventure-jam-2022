using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourceLocator : MonoBehaviour
{
    public static ResourceLocator instance { get; private set; }

    public SceneLoader SceneLoader { get; private set; }
    public UnityAudioManager AudioManager { get; private set; }
    public CutsceneManager CutsceneManager { get; private set; }
    public LetterManager LetterManager { get; private set; }
    public Clock Clock { get; private set; }
    public InventorySystem InventorySystem { get; private set; }
    public GardenManager GardenManager { get; private set; }

    public Eating Eating { get; private set; }

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        // initialize resources
        SceneLoader = FindResourceOfType<SceneLoader>();
        AudioManager = FindResourceOfType<UnityAudioManager>();
        CutsceneManager = FindResourceOfType<CutsceneManager>();
        LetterManager = FindResourceOfType<LetterManager>();
        Clock = FindResourceOfType<Clock>();
        InventorySystem = FindResourceOfType<InventorySystem>();
        GardenManager = FindResourceOfType<GardenManager>();
        Eating = FindResourceOfType<Eating>();
    }

    private T FindResourceOfType<T>() where T : MonoBehaviour {
        // prioritize components on the current game object
        T resource = GetComponent<T>();
        // if no such component, search the scene
        if (resource == null)
            resource = FindObjectOfType<T>();
        return resource;
    }
}