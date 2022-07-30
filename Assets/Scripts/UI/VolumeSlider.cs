using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    private Slider slider;

    private AudioManager audioManager => ResourceLocator.instance.AudioManager;

    private void Start() {
        slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(delegate { audioManager.SetVolume(slider.value); });
    }

    private void Update() {
        slider.value = audioManager.volume;
    }
}
