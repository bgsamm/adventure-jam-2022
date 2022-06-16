using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    //basic controls for music and sound
    [Range(0.0f, 1.0f)]
    [SerializeField]
    private float masterVolume = 1.0f;

    public Slider volumeScale;

    //changing the volume by moving a slider
    public void ChangeVolume() //the UI slider
    {
        masterVolume = volumeScale.value;
        AudioListener.volume = masterVolume; //the editor slider
    }
}
