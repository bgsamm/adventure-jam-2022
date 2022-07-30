using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class AudioManager : MonoBehaviour
{
    public float volume { get; protected set; }

    public abstract void PlayLoop(AudioClip clip, bool withFade = true);
    public abstract void PlayOneShot(AudioClip clip);
    public abstract void SetVolume(float vol);
}
