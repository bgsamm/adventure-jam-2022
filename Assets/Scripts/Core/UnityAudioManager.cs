using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnityAudioManager : AudioManager
{
    public float fadeTime = 0.1f;

    private AudioSource audioSource;

    private void Awake() {
        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();
    }

    public override void PlayLoop(AudioClip clip, bool withFade=true) {
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

    public override void PlayOneShot(AudioClip clip) {
        if (clip != null) {
            audioSource.PlayOneShot(clip);
        }
    }

    public override void SetVolume(float vol) {
        audioSource.volume = vol;
        volume = vol;
    }

    private IEnumerator FadeTo(AudioClip newClip) {
        if (audioSource.isPlaying) {
            // Fade out old clip
            while (audioSource.volume > 0) {
                audioSource.volume -= Time.deltaTime * volume / fadeTime;
                yield return null;
            }
        }
        audioSource.clip = newClip;
        if (newClip != null) {
            audioSource.Play();
            audioSource.volume = 0;
            // Fade in new clip
            while (audioSource.volume < volume) {
                audioSource.volume += Time.deltaTime * volume / fadeTime;
                yield return null;
            }
            audioSource.volume = volume;
        }
        else {
            audioSource.Stop();
        }
    }
}
