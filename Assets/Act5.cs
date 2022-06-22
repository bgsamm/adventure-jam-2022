using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Act5 : MonoBehaviour
{
    public ScreenFader fader;
    public GameObject endPanel;

    private Clock clock => ResourceLocator.instance.Clock;
    private GardenManager gardenManager => ResourceLocator.instance.GardenManager;

    private void Start() {
        endPanel.SetActive(false);
    }

    private void Update() {
        if (gardenManager.SaplingsWatered) {
            fader.FadeOutEvent = delegate {
                endPanel.SetActive(true);
                StartCoroutine(WaitThenEndGame(3));
            };
            fader.FadeOut(true);
        }
    }

    IEnumerator WaitThenEndGame(float s) {
        yield return new WaitForSeconds(s);
        clock.StartNextAct();
    }
}
