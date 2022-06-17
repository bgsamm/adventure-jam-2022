using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnityAudioManager : MonoBehaviour
{
    private AudioSource audioSource;

    private void Awake() {
        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayLoop(AudioClip clip) {
        if (clip != null) {
            audioSource.Stop();
            audioSource.clip = clip;
            audioSource.Play();
        }
    }

    public void PlayOneShot(AudioClip clip) {
        if (clip != null) {
            audioSource.PlayOneShot(clip);
        }
    }
}
