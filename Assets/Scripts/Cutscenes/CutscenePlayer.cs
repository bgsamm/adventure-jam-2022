using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CutscenePlayer : MonoBehaviour
{
    [SerializeField] private Image cutscenePanel;
    [SerializeField] private ScreenFader fader;

    private UnityAudioManager audioManager => ResourceLocator.instance.AudioManager;
    private CutsceneManager cutsceneManager => ResourceLocator.instance.CutsceneManager;
    private Cutscene currentCutscene;

    private int index;

    private void Start() {
        currentCutscene = cutsceneManager.NextCutscene;
        fader.FadeInEvent = delegate {
            StartCoroutine(WaitThenFadeOut(currentCutscene.imageDuration)); 
        };
        fader.FadeOutEvent = ShowNextImage;

        index = 0;
        audioManager.PlayLoop(currentCutscene.audio);
        ShowNextImage();
    }

    private IEnumerator WaitThenFadeOut(float s) {
        yield return new WaitForSeconds(s);
        fader.FadeOut(false);
    }

    private void ShowNextImage() {
        // if images remain, show next image
        if (index < currentCutscene.images.Count) {
            cutscenePanel.sprite = currentCutscene.images[index++];
            fader.FadeIn(false);
        }
        // else, invoke the callback function
        else {
            cutsceneManager.CutsceneEndCallback.Invoke();
        }
    }
}
