using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitySFXManager : MonoBehaviour
{
    private AudioSource audioSource;

    public AudioClip birdsong;

    private void Awake() {
        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayOneShot(AudioClip clip) {
        if (clip != null && !audioSource.isPlaying) {
            audioSource.PlayOneShot(clip);
        }
    }
}
