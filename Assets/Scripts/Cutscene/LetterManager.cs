using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LetterManager : MonoBehaviour
{
    public Letter NextLetter { get; private set; }
    public CallbackEvent LetterEndCallback { get; private set; }

    private SceneLoader sceneLoader;

    private void Start() {
        sceneLoader = ResourceLocator.instance.SceneLoader;
    }

    public void ShowLetter(Letter letter, CallbackEvent callback) {
        if (letter == null) {
            Debug.LogError("Attempted to show null letter");
            return;
        }
        NextLetter = letter;
        LetterEndCallback = callback;
        sceneLoader.LoadLetterScene();
    }
}
