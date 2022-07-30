using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnityAudioManager : MonoBehaviour
{
    private AudioSource audioSource;

    public AudioClip birdsong;

    public AudioClip footstepsGrass;
    public AudioClip footstepsWood;

    public bool onBridge;

    private void Awake() {
        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayLoop(AudioClip clip) {
        audioSource.Stop();
        if (clip != null) {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }

    public void PlayOneShot(AudioClip clip) {
        if (clip != null) {
            audioSource.PlayOneShot(clip);
        }
    }

    public void PauseSound() {
        audioSource.Stop();
    }
}
