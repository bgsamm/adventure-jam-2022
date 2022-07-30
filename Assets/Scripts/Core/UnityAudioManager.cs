using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnityAudioManager : MonoBehaviour
{
    public float fadeTime = 0.1f;

    private AudioSource audioSource;

    //public AudioClip birdsong;
    //public AudioClip footstepsGrass;
    //public AudioClip footstepsWood;
    //public bool onBridge;

    private void Awake() {
        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayLoop(AudioClip clip, bool withFade=true) {
        if (withFade) {
            StartCoroutine(FadeTo(clip));
        }
        else {
            audioSource.Stop();
            audioSource.clip = clip;
            if (clip != null)
                audioSource.Play();
        }
    }

    public void PlayOneShot(AudioClip clip) {
        if (clip != null) {
            audioSource.PlayOneShot(clip);
        }
    }

    private IEnumerator FadeTo(AudioClip newClip) {
        if (audioSource.isPlaying) {
            // Fade out old clip
            while (audioSource.volume > 0) {
                audioSource.volume -= Time.deltaTime / fadeTime;
                yield return null;
            }
        }
        audioSource.clip = newClip;
        if (newClip != null) {
            audioSource.Play();
            audioSource.volume = 0;
            // Fade in new clip
            while (audioSource.volume < 1.0f) {
                audioSource.volume += Time.deltaTime / fadeTime;
                yield return null;
            }
            audioSource.volume = 1.0f;
        }
        else {
            audioSource.Stop();
        }
    }
}
